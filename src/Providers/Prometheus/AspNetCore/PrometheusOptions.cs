using Microsoft.AspNetCore.Http;

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
        public PathString RequestPath { get; set; }
    }
}