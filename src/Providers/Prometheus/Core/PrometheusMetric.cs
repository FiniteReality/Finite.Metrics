namespace Finite.Metrics.Prometheus
{
    internal class PrometheusMetric : IMetric
    {
        private readonly PrometheusMetricStore _metricStore;
        private readonly string _name;

        public PrometheusMetric(PrometheusMetricStore metricStore,
            string name)
        {
            _metricStore = metricStore;
            _name = name;
        }

        public bool IsEnabled()
            => true;

        public void Log<T>(T value, TagValues? tags = null)
            => _metricStore.Store(_name, tags, value);
    }
}
