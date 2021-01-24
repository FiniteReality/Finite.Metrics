using Finite.Metrics.Prometheus.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Finite.Metrics
{
    /// <summary>
    /// Prometheus extensions for <see cref="IMetricsBuilder"/>.
    /// </summary>
    public static class MetricsBuilderAspNetCorePrometheusExtensions
    {
        /// <summary>
        /// Adds Prometheus services to the specified
        /// <see cref="IMetricsBuilder"/>.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMetricsBuilder"/> to add services to.
        /// </param>
        /// <returns>
        /// The <paramref name="builder"/>, to allow for chaining.
        /// </returns>
        /// <remarks>
        /// Remember to call <see cref="PrometheusApplicationBuilderExtensions.UsePrometheus(IApplicationBuilder)"/>
        /// to add the ASP.NET middleware to the request pipeline.
        /// </remarks>
        public static IMetricsBuilder AddPrometheus(
            this IMetricsBuilder builder)
        {
            _ = builder.AddPrometheusCore();

            _ = builder.Services.AddTransient<PrometheusMetricsMiddleware>();

            return builder;
        }
    }
}
