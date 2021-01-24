using System.Net;

namespace Finite.Metrics.Prometheus
{
    /// <summary>
    /// Options to Prometheus metrics middleware.
    /// </summary>
    public class PrometheusOptions
    {
        /// <summary>
        /// Gets or sets the request path that maps to the Prometheus metrics
        /// endpoint.
        /// </summary>
        public string RequestPath { get; set; } = null!;

        /// <summary>
        /// Gets or sets the endpoint that the Prometheus hosted service
        /// listens on.
        /// </summary>
        /// <value></value>
        public IPEndPoint ListenEndPoint { get; set; } = null!;
    }
}
