using Finite.Metrics.Configuration;
using Finite.Metrics.Prometheus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Finite.Metrics
{
    /// <summary>
    /// Prometheus extensions for <see cref="IMetricsBuilder"/>.
    /// </summary>
    public static class MetricsBuilderPrometheusExtensions
    {
        /// <summary>
        /// Adds the minimum essential Prometheus services to the specified
        /// <see cref="IServiceCollection"/>. Additional services, such as
        /// hosted service support, must be added separately using the
        /// <see cref="IMetricsBuilder"/> returned from this method.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMetricsBuilder"/> to add services to.
        /// </param>
        /// <returns>
        /// A <see cref="IMetricsBuilder"/> that can be used to further
        /// configure the Prometheus services.
        /// </returns>
        public static IMetricsBuilder AddPrometheusCore(
            this IMetricsBuilder builder)
        {
            _ = builder.AddConfiguration();

            builder.Services.TryAdd(ServiceDescriptor
                .Singleton<IPrometheusMetricStore, PrometheusMetricStore>());
            builder.Services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IMetricProvider, PrometheusMetricProvider>());

            return builder;
        }
    }
}
