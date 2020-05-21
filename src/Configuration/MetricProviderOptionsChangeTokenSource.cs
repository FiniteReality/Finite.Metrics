using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.Configuration
{
    /// <inheritdoc/>
    public class MetricProviderOptionsChangeTokenSource<TOptions, TProvider>
        : ConfigurationChangeTokenSource<TOptions>
    {
        /// <inheritdoc/>
        public MetricProviderOptionsChangeTokenSource(
            IMetricProviderConfiguration<TProvider> providerConfiguration)
            : base(providerConfiguration.Configuration)
        {
        }
    }
}
