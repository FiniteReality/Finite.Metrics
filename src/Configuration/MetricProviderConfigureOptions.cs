using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.Configuration
{
    /// <inheritdoc/>
    public class MetricProviderConfigureOptions<TOptions, TProvider>
        : ConfigureFromConfigurationOptions<TOptions>
        where TOptions : class
    {
        /// <inheritdoc/>
        public MetricProviderConfigureOptions(
            IMetricProviderConfiguration<TProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {
        }
    }
}
