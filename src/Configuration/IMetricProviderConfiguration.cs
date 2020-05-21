using Microsoft.Extensions.Configuration;

namespace Finite.Metrics.Configuration
{
    /// <summary>
    /// Allows access to the configuration section associated with a metrics
    /// provider.
    /// </summary>
    /// <typeparam name="T">
    /// The type of metrics provider to get configuration for.
    /// </typeparam>
    public interface IMetricProviderConfiguration<T>
    {
        /// <summary>
        /// Gets the configuration section for the requested metrics provider.
        /// </summary>
        IConfiguration Configuration { get; }
    }
}
