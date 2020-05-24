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

        public void Log<T>(T value)
            => throw new NotImplementedException();

        public void Log<T, TTags>(T value, TTags? tags = null)
            where TTags : class
            => throw new NotImplementedException();
    }
}
