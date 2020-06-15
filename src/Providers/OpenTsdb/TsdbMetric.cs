using System.Collections.Generic;
using System.Linq;

namespace Finite.Metrics.OpenTsdb
{
    internal class TsdbMetric : IMetric
    {
        private readonly string _name;
        private readonly TsdbMetricsUploader _uploader;

        public TsdbMetric(string name, TsdbMetricsUploader uploader)
        {
            _name = name;
            _uploader = uploader;
        }

        public bool IsEnabled()
            => true;

        public void Log<T>(T value, TagValues? tags = null)
            => _uploader.AddLogEntry(new TsdbPutRequest
            {
                Metric = _name,
                Value = value!,
                Tags = tags?.ToDictionary(x => x.Key, x => x.Value)
                    ?? new Dictionary<string, object?>()
            });
    }
}
