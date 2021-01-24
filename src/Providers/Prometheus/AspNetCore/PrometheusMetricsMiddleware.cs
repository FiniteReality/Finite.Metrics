using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.Prometheus.AspNetCore
{
    internal class PrometheusMetricsMiddleware : IMiddleware
    {
        private readonly ILogger<PrometheusMetricsMiddleware> _logger;
        private readonly IPrometheusMetricStore _metricStore;
        private readonly PrometheusOptions _options;

        public PrometheusMetricsMiddleware(
            ILogger<PrometheusMetricsMiddleware> logger,
            IPrometheusMetricStore metricStore,
            IOptions<PrometheusOptions> options)
        {
            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
            _metricStore = metricStore
                ?? throw new ArgumentNullException(nameof(metricStore));
            _options = options?.Value
                ?? throw new ArgumentNullException(nameof(options));
        }

        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.GetEndpoint() != null)
            {
                _logger.LogDebug(
                    "Skipped Prometheus metrics as an endpoint was matched");
            }
            else if (!HttpMethods.IsGet(context.Request.Method)
                && !HttpMethods.IsHead(context.Request.Method))
            {
                _logger.LogDebug(
                    "Skipped Prometheus metrics as {Method} is not supported",
                        context.Request.Method);
            }
            else if (!context.Request.Path.StartsWithSegments(
                _options.RequestPath))
            {
                _logger.LogDebug(
                    "Skipped Prometheus metrics as {Path} was not matched",
                        context.Request.Path);
            }
            else
            {
                return WriteMetricsAsync(context);
            }

            return next(context);
        }

        private async Task WriteMetricsAsync(HttpContext context)
        {
            foreach (var metric in _metricStore.GetMetrics())
            {
                await context.Response.WriteAsync(metric,
                    context.RequestAborted);
                await context.Response.WriteAsync("\n",
                    context.RequestAborted);
            }
        }
    }
}
