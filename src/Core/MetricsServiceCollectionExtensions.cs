using System;
using Finite.Metrics;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for setting up metrics services in an
    /// <see cref="IServiceCollection"/>.
    /// </summary>
    public static class MetricsServiceCollectionExtensions
    {
        /// <summary>
        /// Adds metrics services to the specified
        /// <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can
        /// be chained.
        /// </returns>
        public static IMetricsBuilder AddMetrics(
            this IServiceCollection services)
        {
            if (services is null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor
                .Singleton<IMetricFactory, MetricFactory>());
            services.TryAdd(ServiceDescriptor.Singleton<IMetric, Metric>());

            return new MetricsBuilder(services);
        }
    }
}
