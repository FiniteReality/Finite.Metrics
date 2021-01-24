using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.Prometheus.HostedService
{
    internal class PrometheusMetricsHostedService : IHostedService
    {
        private readonly IServer _server;

        private readonly PrometheusApplication _application;

        public PrometheusMetricsHostedService(
            ILogger<PrometheusMetricsHostedService> logger,
            IOptionsMonitor<PrometheusOptions> options,
            IPrometheusMetricStore store,
            IServer server)
        {
            _server = server;

            _application = new PrometheusApplication(logger, options, store);
        }

        public async Task StartAsync(CancellationToken cancellationToken)
            => await _server.StartAsync(_application, cancellationToken);

        public async Task StopAsync(CancellationToken cancellationToken)
            => await _server.StopAsync(cancellationToken);
    }
}
