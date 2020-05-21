using System;

namespace Finite.Metrics
{
    /// <summary>
    /// Represents a type that can create instances of <see cref="IMetric"/>.
    /// </summary>
    public interface IMetricProvider : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="IMetric"/> instance.
        /// </summary>
        /// <param name="metricName">
        /// The name for metrics produced by the metric.
        /// </param>
        /// <returns>
        /// The instance of <see cref="IMetric"/> that was created.
        /// </returns>
        IMetric CreateMetric(string metricName);
    }
}
