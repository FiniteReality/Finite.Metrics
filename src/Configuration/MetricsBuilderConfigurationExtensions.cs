using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Finite.Metrics.Configuration
{
    /// <summary>
    /// Extension methods for setting up metrics services in an
    /// <see cref="IMetricsBuilder"/>.
    /// </summary>
    public static class MetricsBuilderConfigurationExtensions
    {
        /// <summary>
        /// Adds services required to consume
        /// <see cref="IMetricProviderConfigurationFactory"/> or
        /// <see cref="IMetricProviderConfiguration{T}"/>
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMetricsBuilder"/> to register services on.
        /// </param>
        /// <returns>
        /// The builder.
        /// </returns>
        public static IMetricsBuilder AddConfiguration(
            this IMetricsBuilder builder)
        {
            builder.Services
                .TryAddSingleton<IMetricProviderConfigurationFactory,
                    MetricProviderConfigurationFactory>();

            builder.Services.TryAddSingleton(
                typeof(IMetricProviderConfiguration<>),
                typeof(MetricProviderConfiguration<>));

            return builder;
        }
    }
}
