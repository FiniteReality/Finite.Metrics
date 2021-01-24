using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.Prometheus.HostedService
{
    internal partial class PrometheusApplication
        : IHttpApplication<PrometheusApplication.Context>
    {
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<PrometheusOptions> _options;
        private readonly IPrometheusMetricStore _store;

        public PrometheusApplication(
            ILogger logger,
            IOptionsMonitor<PrometheusOptions> options,
            IPrometheusMetricStore store)
        {
            _logger = logger;
            _options = options;
            _store = store;
        }

        public Context CreateContext(IFeatureCollection contextFeatures)
        {
            var request = contextFeatures.Get<IHttpRequestFeature>();
            var response = contextFeatures.Get<IHttpResponseFeature>();
            var responseBody = contextFeatures.Get<IHttpResponseBodyFeature>();

            return new Context(request, response, responseBody);
        }

        public void DisposeContext(Context context, Exception exception)
        {
            if (exception is not null)
            {
                _logger.LogError(exception,
                    "Exception occured during request: {message}",
                    exception.Message);
            }
        }

        public Task ProcessRequestAsync(Context context)
        {
            var options = _options.CurrentValue;

            if (!context.RequestPath.StartsWith(options.RequestPath))
            {
                context.StatusCode = 404;
                context.Write("Not found");

                return Task.CompletedTask;
            }

            context.StatusCode = 200;

            foreach (var metric in _store.GetMetrics())
            {
                context.Write(metric);
                context.Write("\n");
            }

            return Task.CompletedTask;
        }
    }
}
