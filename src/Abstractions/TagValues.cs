using System;
using System.Collections;
using System.Collections.Generic;

namespace Finite.Metrics
{
    /// <summary>
    /// Represents a collection of tag keys and values
    /// </summary>
    public partial class TagValues
        : IReadOnlyList<KeyValuePair<string, object?>>
    {
        private readonly List<KeyValuePair<string, object?>> _tags;

        private TagValues(List<KeyValuePair<string, object?>> tags)
        {
            _tags = tags;
        }

        /// <summary>
        /// Creates a new instance of <see cref="TagValues"/> from the given
        /// object.
        /// </summary>
        /// <param name="value">
        /// The value to convert to a collection of tag keys and values.
        /// </param>
        /// <typeparam name="T">
        /// The type of value to convert.
        /// </typeparam>
        /// <returns>
        /// A <see cref="TagValues"/> containing the data specified by
        /// <paramref name="value"/>.
        /// </returns>
        public static TagValues CreateFrom<T>(T value)
            where T : class
            => new TagValues(
                new List<KeyValuePair<string, object?>>(
                    PropertiesHelper<T>.GetProps(value)));

        /// <inheritdoc/>
        public KeyValuePair<string, object?> this[int index]
            => _tags[index];

        /// <inheritdoc/>
        public int Count
            => _tags.Count;

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator()
            => _tags.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
            => ((IEnumerable)_tags).GetEnumerator();
    }
}
