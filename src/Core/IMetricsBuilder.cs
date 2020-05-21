using System;
using Microsoft.Extensions.DependencyInjection;

namespace Finite.Metrics
{
    /// <summary>
    /// An interface for configuring metrics providers.
    /// </summary>
    public interface IMetricsBuilder
    {
        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> where metrics services
        /// are configured.
        /// </summary>
        IServiceCollection Services { get; }
    }
}
