using Microsoft.AspNetCore.Http;

namespace Finite.Metrics.Prometheus.AspNetCore
{
    /// <summary>
    /// Options to Prometheus metrics middleware.
    /// </summary>
    public class PrometheusOptions
    {
        /// <summary>
        /// The request path that maps to the Prometheus metrics endpoint.
        /// </summary>
        public PathString RequestPath { get; set; }
    }
}
