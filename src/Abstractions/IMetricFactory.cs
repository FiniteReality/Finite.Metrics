using System;

namespace Finite.Metrics
{
    /// <summary>
    /// Represents a type used to configure the metrics system and create
    /// instances of <see cref="IMetric"/> form the registered
    /// <see cref="IMetricProvider"/>s.
    /// </summary>
    public interface IMetricFactory : IDisposable
    {
        /// <summary>
        /// Creates a new <see cref="IMetric"/> instance.
        /// </summary>
        /// <param name="metricName">
        /// The name of metrics produced by the metric.
        /// </param>
        /// <returns>
        /// The <see cref="IMetric"/>.
        /// </returns>
        IMetric CreateMetric(string metricName);

        /// <summary>
        /// Adds an <see cref="IMetricProvider"/> to the metrics system.
        /// </summary>
        /// <param name="provider">
        /// The <see cref="IMetricProvider"/>.
        /// </param>
        void AddProvider(IMetricProvider provider);
    }
}
