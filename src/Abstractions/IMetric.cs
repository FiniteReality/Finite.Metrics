﻿using System;

namespace Finite.Metrics
{
    /// <summary>
    /// Represents a type used to record metrics.
    /// </summary>
    public interface IMetric
    {
        /// <summary>
        /// Checks if the given metric is enabled.
        /// </summary>
        /// <returns>
        /// <c>true</c> if enabled.
        /// </returns>
        bool IsEnabled();

        /// <summary>
        /// Writes a metrics entry.
        /// </summary>
        /// <param name="value">
        /// The value that will be written.
        /// </param>
        /// <param name="tags">
        /// The additional tags to supply with this metric entry.
        /// </param>
        /// <typeparam name="T">
        /// The type of value to write.
        /// </typeparam>
        void Log<T>(T value, TagValues? tags = null);
    }
}
