using System;
using Microsoft.Extensions.Configuration;

namespace Finite.Metrics.Configuration
{
    /// <summary>
    /// Allows access to the configuration section associated with a metrics
    /// provider.
    /// </summary>
    public interface IMetricProviderConfigurationFactory
    {
        /// <summary>
        /// Gets the configuration section associated with the passed metrics
        /// provider.
        /// </summary>
        /// <param name="providerType">
        /// The metrics provider type.
        /// </param>
        /// <returns>
        /// The <see cref="IConfiguration"/> for the given
        /// <paramref name="providerType"/>.
        /// </returns>
        IConfiguration GetConfiguration(
            Type providerType);
    }
}
