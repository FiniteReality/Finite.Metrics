using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Finite.Metrics.Prometheus
{
    /// <summary>
    /// Represents a store of prometheus metrics.
    /// </summary>
    public interface IPrometheusMetricStore
    {
        /// <summary>
        /// Stores a metric for later scraping.
        /// </summary>
        /// <param name="name">
        /// The name of the metric.
        /// </param>
        /// <param name="tags">
        /// The tags to store with this metric.
        /// </param>
        /// <param name="value">
        /// The value to store for this metric.
        /// </param>
        /// <typeparam name="T">
        /// The type of <paramref name="value"/>.
        /// </typeparam>
        void Store<T>(string name, TagValues? tags, T value);

        /// <summary>
        /// Gets all of the metrics converted to Prometheus' text-based
        /// exposition format.
        /// </summary>
        /// <returns>
        /// An enumerable containing the ordered, unique metrics.
        /// </returns>
        IEnumerable<string> GetMetrics();
    }
}
