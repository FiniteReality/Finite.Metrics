using System;
using NUnit.Framework;

namespace Finite.Metrics.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="Metric"/>.
    /// </summary>
    public class MetricTests
    {
        /// <summary>
        /// Ensures that <see cref="Metric.IsEnabled"/> returns false when no
        /// provider metrics are present.
        /// </summary>
        [Test]
        public void IsEnabledReturnsFalseForNoMetrics()
        {
            var metric = new Metric();

            Assert.False(metric.IsEnabled()); // metric.Metrics = null

            metric.Metrics = Array.Empty<IMetric>();

            Assert.False(metric.IsEnabled()); // metric.Metrics.Length == 0
        }

        /// <summary>
        /// Ensures that <see cref="Metric.IsEnabled"/> returns true when a
        /// provider metric is present and enabled.
        /// </summary>
        [Test]
        public void IsEnabledReturnsTrueForEnabledMetric()
        {
            var metric = new Metric
            {
                Metrics = new[] { new EnabledButThrowingMetric() }
            };

            Assert.True(metric.IsEnabled());
        }

        /// <summary>
        /// Ensures that <see cref="Metric.IsEnabled"/> returns false when all
        /// provider metrics are disabled.
        /// </summary>
        [Test]
        public void IsEnabledReturnsFalseForDisabledMetrics()
        {
            var metric = new Metric
            {
                Metrics = new[] { new DisabledAndThrowingMetric() }
            };

            Assert.False(metric.IsEnabled());
        }


        /// <summary>
        /// Ensures that <see cref="Metric.IsEnabled"/> throws an instance of
        /// <see cref="AggregateException"/> if any of the metrics throw, and
        /// that the thrown exception contains all of the correct exceptions.
        /// </summary>
        [Test]
        public void IsEnabledThrowsAggregateException()
        {
            var metric = new Metric
            {
                Metrics = new[] { new ThrowingMetric() }
            };

            var exception = Assert.Throws<AggregateException>(
                () => metric.IsEnabled());

            Assert.AreEqual(1, exception.InnerExceptions.Count);
            Assert.IsInstanceOf<NotImplementedException>(
                exception.InnerExceptions[0]);
        }

        /// <summary>
        /// Ensures that <see cref="Metric.Log{T, TTags}(T, TTags)"/> does
        /// nothing when <see cref="Metric.IsEnabled"/> return false.
        /// </summary>
        [Test]
        public void LogWhenDisabledIsIgnored()
        {
            var metric = new Metric
            {
                Metrics = new[] { new DisabledAndThrowingMetric() }
            };

            // Parameters here should not matter as they should be passed right
            // through to the underlying provider metric, if it's enabled.
            Assert.DoesNotThrow(() => metric.Log(1, metric));
        }

        /// <summary>
        /// Ensures that <see cref="Metric.Log{T, TTags}(T, TTags)"/> throws an
        /// instance of <see cref="AggregateException"/> if any of the metrics
        /// throw, and that the thrown exception contains all of the correct
        /// exceptions.
        /// </summary>
        [Test]
        public void LogWhenEnabledThrowsAggregateException()
        {
            var metric = new Metric
            {
                Metrics = new[] { new EnabledButThrowingMetric() }
            };


            var exception = Assert.Throws<AggregateException>(
                () => metric.Log(1, metric));

            Assert.AreEqual(1, exception.InnerExceptions.Count);
            Assert.IsInstanceOf<NotImplementedException>(
                exception.InnerExceptions[0]);
        }
    }
}
