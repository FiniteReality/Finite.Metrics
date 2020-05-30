using System;
using System.Diagnostics;

namespace Finite.Metrics
{
    /// <summary>
    /// Contains helper methods for <see cref="IMetric"/>, to make measuring
    /// certain metrics easier.
    /// </summary>
    public static partial class Measure
    {
        /// <summary>
        /// Measures a duration, starting at the method call, finishing when
        /// <see cref="IDisposable.Dispose"/> is called.
        /// </summary>
        /// <param name="metric">
        /// The metric to store the result in.
        /// </param>
        /// <returns>
        /// </returns>
        public static DurationMeasure Duration(IMetric metric)
            => new DurationMeasure(metric);

        /// <summary>
        /// Measures a duration, starting at the method call, finishing when
        /// <see cref="IDisposable.Dispose"/> is called.
        /// </summary>
        /// <param name="metric">
        /// The metric to store the result in.
        /// </param>
        /// <param name="tags">
        /// The optional tags to tag this duration with.
        /// </param>
        /// <typeparam name="TTags">
        /// The type of the tags to tag this duration with.
        /// </typeparam>
        /// <returns>
        /// </returns>
        public static DurationMeasure<TTags> Duration<TTags>(IMetric metric,
            TTags tags)
            where TTags : class
            => new DurationMeasure<TTags>(metric, tags);

        /// <summary>
        /// A duration measure for measuring durations using
        /// <see cref="Duration(IMetric)"/>.
        /// </summary>
        public struct DurationMeasure : IDisposable
        {
            private readonly IMetric _metric;
            private readonly Stopwatch _timer;

            internal DurationMeasure(IMetric metric)
            {
                if (metric is null)
                    throw new ArgumentNullException(nameof(metric));

                _metric = metric;
                _timer = Stopwatch.StartNew();
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                var time = _timer.Elapsed;
                _timer.Stop();

                _metric.Log(time);
            }
        }

        /// <summary>
        /// A duration measure for measuring durations using
        /// <see cref="Duration{TTags}(IMetric, TTags)"/>.
        /// </summary>
        /// <typeparam name="TTags">
        /// The type of the tags used to tag the duration with.
        /// </typeparam>
        public struct DurationMeasure<TTags> : IDisposable
            where TTags : class
        {
            private readonly IMetric _metric;
            private readonly TTags _tags;
            private readonly Stopwatch _timer;

            internal DurationMeasure(IMetric metric, TTags tags)
            {
                if (metric is null)
                    throw new ArgumentNullException(nameof(metric));

                _metric = metric;
                _tags = tags;
                _timer = Stopwatch.StartNew();
            }

            /// <inheritdoc/>
            public void Dispose()
            {
                var time = _timer.Elapsed;
                _timer.Stop();

                _metric.Log(time, _tags);
            }
        }
    }
}
