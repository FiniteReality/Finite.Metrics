namespace Finite.Metrics
{
    /// <summary>
    /// Extension methods for <see cref="IMetric"/> for common scenarios.
    /// </summary>
    public static class MetricExtensions
    {
        /// <summary>
        /// Writes a metric entry.
        /// </summary>
        /// <param name="metric">
        /// The <see cref="IMetric"/> to write to.
        /// </param>
        /// <param name="value">
        /// The value that will be written.
        /// </param>
        /// <param name="tags">
        /// The tags that will be supplied with this metric entry.
        /// </param>
        /// <typeparam name="T">
        /// The type of value to write.
        /// </typeparam>
        /// <typeparam name="TTags">
        /// The type of tags to supply with the metric.
        /// </typeparam>
        public static void Log<T, TTags>(this IMetric metric, T value,
            TTags tags)
            where TTags : class
            => metric.Log(value, TagValues.CreateFrom(tags));
    }
}
