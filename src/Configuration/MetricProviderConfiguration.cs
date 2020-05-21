using Microsoft.Extensions.Configuration;

namespace Finite.Metrics.Configuration
{
    internal class MetricProviderConfiguration<T>
        : IMetricProviderConfiguration<T>
    {
        public IConfiguration Configuration { get; }

        public MetricProviderConfiguration(
            IMetricProviderConfigurationFactory providerConfigurationFactory)
        {
            Configuration = providerConfigurationFactory
                .GetConfiguration(typeof(T));
        }
    }
}
