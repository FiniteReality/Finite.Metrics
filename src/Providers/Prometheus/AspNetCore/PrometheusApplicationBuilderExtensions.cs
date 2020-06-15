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

        /// <summary>
        /// Enables Prometheus metrics endpoints for the given request path.
        /// </summary>
        /// <param name="app">
        /// The <see cref="IApplicationBuilder"/> to enable Prometheus metrics
        /// endpoints for.
        /// </param>
        /// <param name="requestPath">
        /// The relative request path to serve metrics for.
        /// </param>
        /// <returns>
        /// The <see cref="IApplicationBuilder"/> passed in
        /// <paramref name="app"/>, for chaining.
        /// </returns>
        public static IApplicationBuilder UsePrometheus(
            this IApplicationBuilder app,
            string requestPath)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app));
            if (requestPath is null)
                throw new ArgumentNullException(nameof(requestPath));

            return app.UseMiddleware<PrometheusMetricsMiddleware>(
                new PrometheusOptions
                {
                    RequestPath = requestPath
                });
        }

        /// <summary>
        /// Enables Prometheus metrics endpoints for the given request path.
        /// </summary>
        /// <param name="app">
        /// The <see cref="IApplicationBuilder"/> to enable Prometheus metrics
        /// endpoints for.
        /// </param>
        /// <param name="options">
        /// The options to use when serving Prometheus metrics.
        /// </param>
        /// <returns>
        /// The <see cref="IApplicationBuilder"/> passed in
        /// <paramref name="app"/>, for chaining.
        /// </returns>
        public static IApplicationBuilder UsePrometheus(
            this IApplicationBuilder app,
            PrometheusOptions options)
        {
            if (app is null)
                throw new ArgumentNullException(nameof(app));
            if (options is null)
                throw new ArgumentNullException(nameof(options));

            return app.UseMiddleware<PrometheusMetricsMiddleware>(
                Options.Create(options));
        }
    }
}
