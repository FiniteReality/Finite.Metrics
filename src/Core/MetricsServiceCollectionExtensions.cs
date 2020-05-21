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
        public static IServiceCollection AddMetrics(
            this IServiceCollection services)
            => AddMetrics(services, builder => { });


        /// <summary>
        /// Adds metrics services to the specified
        /// <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add services to.
        /// </param>
        /// <param name="configure">
        /// The <see cref="IMetricsBuilder"/> configuration delegate.
        /// </param>
        /// <returns>
        /// The <see cref="IServiceCollection"/> so that additional calls can
        /// be chained.
        /// </returns>
        public static IServiceCollection AddMetrics(
            this IServiceCollection services,
            Action<IMetricsBuilder> configure)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.TryAdd(ServiceDescriptor
                .Singleton<IMetricFactory, MetricFactory>());
            services.TryAdd(ServiceDescriptor.Singleton<IMetric, Metric>());

            configure(new MetricsBuilder(services));
            return services;
        }
    }
}
