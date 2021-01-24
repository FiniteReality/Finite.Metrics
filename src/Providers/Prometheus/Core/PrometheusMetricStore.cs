using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Finite.Metrics.Prometheus
{
    internal class PrometheusMetricStore : IPrometheusMetricStore
    {
        private readonly ConcurrentQueue<(string, TagValues?, object?)> _items;

        public PrometheusMetricStore()
        {
            _items = new ConcurrentQueue<(string, TagValues?, object?)>();
        }

        public void Store<T>(string name, TagValues? tags, T value)
            => _items.Enqueue((name, tags, value));

        public IEnumerable<string> GetMetrics()
            => GetUniqueMetrics()
                .OrderByDescending(x => x.name)
                .Select(x => $"{x.name} {x.value}");

        internal IEnumerable<(string name, object? value)> GetUniqueMetrics()
        {
            var existing = new HashSet<string>();

            var metrics = GetRawMetrics();

            foreach (var metric in metrics)
            {
                if (!existing.Add(metric.name))
                    continue;

                yield return metric;
            }
        }

        internal IEnumerable<(string name, object? value)> GetRawMetrics()
        {
            while (_items.TryDequeue(out var metric))
            {
                var (name, tags, value) = metric;

                if (tags != null)
                {
                    var tagsString = string.Join(",",
                        tags.Select(x => $"{x.Key}=\"{x.Value}\""));

                    yield return ($"{name}{{{tagsString}}}", value);
                }
                else
                {
                    yield return (name, value);
                }
            }
        }
    }
}
