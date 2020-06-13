using System;
using System.Collections.Generic;
using System.Reflection;

namespace Finite.Metrics
{
    public partial class TagValues
    {
        private static class PropertiesHelper<T>
            where T : class
        {
            private static readonly List<(string, Func<T, object?>)> Props
                = new List<(string, Func<T, object?>)>();
            private static readonly MethodInfo GetValueFactoryMethod
                = typeof(PropertiesHelper<T>).GetMethod(
                    nameof(GetValueFactory),
                    BindingFlags.NonPublic | BindingFlags.Static)!;

            static PropertiesHelper()
            {
                var props = typeof(T).GetProperties();

                foreach (var prop in props)
                {
                    if (prop.GetMethod is null)
                        continue;

                    var getMethod = GetValueFactoryMethod
                        .MakeGenericMethod(new[] { prop.PropertyType });
                    var getter = (Func<T, object?>)getMethod
                        .Invoke(null, new[] { prop.GetMethod })!;

                    Props.Add((prop.Name, getter));
                }
            }

            public static IEnumerable<KeyValuePair<string, object?>> GetProps(
                T value)
            {
                foreach (var prop in Props)
                {
                    yield return KeyValuePair.Create(
                        prop.Item1, prop.Item2(value));
                }
            }

            private static Func<T, object?> GetValueFactory<TProperty>(
                MethodInfo getMethod)
            {
                var method = (Func<T, TProperty>)getMethod
                    .CreateDelegate(typeof(Func<T, TProperty>));

                return v => method(v);
            }
        }
    }
}
