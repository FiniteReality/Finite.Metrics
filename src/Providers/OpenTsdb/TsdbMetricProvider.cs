namespace Finite.Metrics.OpenTsdb
{
    internal class TsdbMetricProvider : IMetricProvider
    {
        private readonly TsdbMetricsUploader _uploader;

        public TsdbMetricProvider(TsdbMetricsUploader uploader)
        {
            _uploader = uploader;
        }

        public IMetric CreateMetric(string name)
            => new TsdbMetric(name, _uploader);

        public void Dispose()
        {
            // no-op
        }
    }
}
