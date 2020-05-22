using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finite.Metrics.Configuration
{
    /// <summary>
    /// Extension methods for setting up metrics services in an
    /// <see cref="IMetricsBuilder"/>.
    /// </summary>
    public static class MetricsBuilderExtensions
    {
        /// <summary>
        /// Configures an <see cref="IMetricsBuilder"/> from an instance of
        /// <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMetricsBuilder"/> to use.
        /// </param>
        /// <param name="configuration">
        /// The <see cref="IConfiguration"/> to add.
        /// </param>
        /// <returns>
        /// The builder.
        /// </returns>
        public static IMetricsBuilder AddConfiguration(
            this IMetricsBuilder builder,
            IConfiguration configuration)
        {
            _ = builder.AddConfiguration();

            _ = builder.Services.AddSingleton(
                new MetricsConfiguration(configuration));

            return builder;
        }
    }
}
