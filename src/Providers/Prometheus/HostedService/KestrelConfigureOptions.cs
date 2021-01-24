using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.Prometheus.HostedService
{
    internal class KestrelConfigureOptions
        : IConfigureOptions<KestrelServerOptions>
    {
        private readonly IOptions<PrometheusOptions> _options;

        public KestrelConfigureOptions(IOptions<PrometheusOptions> options)
        {
            _options = options;
        }

        public void Configure(KestrelServerOptions options)
            => options.Listen(_options.Value.ListenEndPoint);
    }
}
