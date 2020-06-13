using System;

namespace Finite.Metrics.UnitTests
{
    /// <summary>
    /// A metric which is enabled, but whose Log method throws.
    /// </summary>
    internal class EnabledButThrowingMetric : IMetric
    {
        public EnabledButThrowingMetric()
        { }

        public bool IsEnabled()
            => true;

        public void Log<T>(T value, TagValues? tags = null)
            => throw new NotImplementedException();
    }
}
