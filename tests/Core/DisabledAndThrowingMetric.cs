using System;

namespace Finite.Metrics.UnitTests
{
    /// <summary>
    /// A metric which is disabled, and whose Log method throws.
    /// </summary>
    internal class DisabledAndThrowingMetric : IMetric
    {
        public DisabledAndThrowingMetric()
        { }

        public bool IsEnabled()
            => false;

        public void Log<T>(T value, TagValues? tags = null)
            => throw new NotImplementedException();
    }
}
