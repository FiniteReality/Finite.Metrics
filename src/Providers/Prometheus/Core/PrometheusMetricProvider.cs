namespace Finite.Metrics.Prometheus
{
    internal class PrometheusMetricProvider : IMetricProvider
    {
        private readonly IPrometheusMetricStore _metricStore;

        public PrometheusMetricProvider(
            IPrometheusMetricStore metricStore)
        {
            _metricStore = metricStore;
        }

        public IMetric CreateMetric(string metricName)
            => new PrometheusMetric(_metricStore, metricName);

        public void Dispose()
        { /* no-op */ }
    }
}
