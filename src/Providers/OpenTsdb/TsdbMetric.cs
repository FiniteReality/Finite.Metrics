using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using ToDict = System.Func<object?,
    System.Collections.Generic.IDictionary<string, string>?>;

namespace Finite.Metrics.OpenTsdb
{
    internal class TsdbMetric : IMetric
    {
        private static readonly ConcurrentDictionary<Type, ToDict> TagsGetters
            = new ConcurrentDictionary<Type, ToDict>();

        private readonly string _name;
        private readonly TsdbMetricsUploader _uploader;

        public TsdbMetric(string name, TsdbMetricsUploader uploader)
        {
            _name = name;
            _uploader = uploader;
        }

        public bool IsEnabled()
        {
            return true;
        }

        public void Log<T, TTags>(T value, TTags? tags = null)
            where TTags : class
        {
            var getter = TagsGetters.GetOrAdd(typeof(TTags), CreateTagGetter);

            _uploader.AddLogEntry(new TsdbPutRequest
            {
                Metric = _name,
                Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Value = value!,
                Tags = getter(tags)
            });

            static ToDict CreateTagGetter(Type type)
            {
                var properties = type.GetProperties()
                    .Where(x => x.GetMethod != null);
                var getters = properties.Select(
                    x => new {
                        Method = (Func<object, object>)x.GetMethod!
                            .CreateDelegate(typeof(Func<object, object>)),
                        x.Name
                    });

                return (o) =>
                {
                    var dict = new Dictionary<string, string>();

                    if (o is {})
                        foreach (var getter in getters)
                        {
                            dict[getter.Name]
                                = getter.Method(o).ToString() ?? "";
                        }

                    return dict;
                };
            }
        }
    }
}
