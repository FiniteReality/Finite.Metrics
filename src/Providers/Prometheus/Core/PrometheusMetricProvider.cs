namespace Finite.Metrics.Prometheus
{
    internal class PrometheusMetricProvider : IMetricProvider
    {
        private readonly PrometheusMetricStore _metricStore;

        public PrometheusMetricProvider(
            PrometheusMetricStore metricStore)
        {
            _metricStore = metricStore;
        }

        public IMetric CreateMetric(string metricName)
            => new PrometheusMetric(_metricStore, metricName);

        public void Dispose()
        { /* no-op */ }
    }
}
