using Finite.Metrics.Configuration;
using Finite.Metrics.Prometheus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Finite.Metrics
{
    /// <summary>
    /// Prometheus extensions for <see cref="IMetricsBuilder"/>.
    /// </summary>
    public static class MetricsBuilderOpenTsdbExtensions
    {
        /// <summary>
        /// Adds a Prometheus provider to the factory.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMetricsBuilder AddPrometheus(
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
