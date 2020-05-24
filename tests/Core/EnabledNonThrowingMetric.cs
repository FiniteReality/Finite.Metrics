using System;

namespace Finite.Metrics.UnitTests
{
    /// <summary>
    /// A metric which is enabled, and whose Log methods do not throw.
    /// </summary>
    internal class EnabledNonThrowingMetric : IMetric
    {
        public EnabledNonThrowingMetric()
        { }

        public bool IsEnabled()
            => true;

        public void Log<T>(T value)
        { }

        public void Log<T, TTags>(T value, TTags? tags = null)
            where TTags : class
        { }
    }
}
