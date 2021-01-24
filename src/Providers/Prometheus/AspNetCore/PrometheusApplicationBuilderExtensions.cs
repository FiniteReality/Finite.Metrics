using System;
using Finite.Metrics.Prometheus.AspNetCore;
using Microsoft.Extensions.Options;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// Extension methods for Finite.Metrics.Prometheus.
    /// </summary>
    public static class PrometheusApplicationBuilderExtensions
    {
        /// <summary>
        /// Enables Prometheus metrics endpoints for the current request path.
        /// </summary>
        /// <param name="app">
        /// The <see cref="IApplicationBuilder"/> to enable Prometheus metrics
        /// endpoints for.
        /// </param>
        /// <returns>
        /// The <see cref="IApplicationBuilder"/> passed in
        /// <paramref name="app"/>, for chaining.
        /// </returns>
        public static IApplicationBuilder UsePrometheus(
            this IApplicationBuilder app)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<PrometheusMetricsMiddleware>();
        }
    }
}
