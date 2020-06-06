using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

using PropertyGetter = System.Func<object, object>;
using ToDict = System.Func<object?,
    System.Collections.Generic.IDictionary<string, object>>;

namespace Finite.Metrics.OpenTsdb
{
    internal class TsdbMetric : IMetric
    {
        private static readonly ConcurrentDictionary<Type, ToDict> TagsGetters
            = new ConcurrentDictionary<Type, ToDict>();
        private static readonly ConcurrentDictionary<
            (Type, Type), PropertyGetter> PropertyGetterHelpers
                = new ConcurrentDictionary<(Type, Type), PropertyGetter>();

        private readonly string _name;
        private readonly TsdbMetricsUploader _uploader;

        public TsdbMetric(string name, TsdbMetricsUploader uploader)
        {
            _name = name;
            _uploader = uploader;
        }

        public bool IsEnabled()
            => true;

        public void Log<T>(T value)
        {
            _uploader.AddLogEntry(new TsdbPutRequest
            {
                Metric = _name,
                Value = value!,
                Tags = new Dictionary<string, object>()
            });
        }

        public void Log<T, TTags>(T value, TTags? tags = null)
            where TTags : class
        {
            var getter = TagsGetters.GetOrAdd(typeof(TTags), CreateTagGetter);

            _uploader.AddLogEntry(new TsdbPutRequest
            {
                Metric = _name,
                Value = value!,
                Tags = getter(tags)
            });

            static ToDict CreateTagGetter(Type type)
            {
                var properties = type.GetProperties()
                    .Where(x => x.GetMethod != null);
                var getters = properties.Select(
                    x => new {
                        Method = PropertyGetterHelpers
                            .GetOrAdd((x.DeclaringType!, x.PropertyType),
                                (_) => MakeGetter(x.GetMethod!)),
                        x.Name
                    });

                return (o) =>
                {
                    var dict = new Dictionary<string, object>();

                    if (o is {})
                        foreach (var getter in getters)
                        {
                            dict[getter.Name] = getter.Method(o);
                        }

                    return dict;
                };
            }

            static PropertyGetter MakeGetter(MethodInfo method)
            {
                var helper = MakeGetterHelperMethod.MakeGenericMethod(new []{
                    method.DeclaringType!,
                    method.ReturnType!
                });

                return (PropertyGetter)helper!.Invoke(null, new[] {
                    method
                })!;
            }
        }

        private static readonly MethodInfo MakeGetterHelperMethod
            = typeof(TsdbMetric).GetMethod(nameof(MakeGetterHelper),
                BindingFlags.NonPublic | BindingFlags.Static)!;
        private static PropertyGetter MakeGetterHelper<TType, TValue>(
            MethodInfo method)
        {
            var func = (Func<TType, TValue>)method
                .CreateDelegate(typeof(Func<TType, TValue>));

            return (o) => func((TType)o)!;
        }
    }
}
