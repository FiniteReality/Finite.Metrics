using System.Threading;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using IApplicationLifetime = Microsoft.AspNetCore.Hosting.IApplicationLifetime;

namespace Finite.Metrics.Prometheus.HostedService
{
#pragma warning disable CS0618
    internal class PrometheusApplicationLifetime : IApplicationLifetime
#pragma warning restore CS0618
    {
        private readonly IHostApplicationLifetime _applicationLifetime;
        public PrometheusApplicationLifetime(
            IHostApplicationLifetime applicationLifetime)
        {
            _applicationLifetime = applicationLifetime;
        }

        public CancellationToken ApplicationStarted
            => _applicationLifetime.ApplicationStarted;

        public CancellationToken ApplicationStopping
            => _applicationLifetime.ApplicationStopping;

        public CancellationToken ApplicationStopped
            => _applicationLifetime.ApplicationStopped;

        public void StopApplication()
            => _applicationLifetime.StopApplication();
    }
}
