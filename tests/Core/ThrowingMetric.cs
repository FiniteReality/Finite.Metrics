using System;

namespace Finite.Metrics.UnitTests
{
    /// <summary>
    /// A metric whose methods all throw.
    /// </summary>
    internal class ThrowingMetric : IMetric
    {
        public ThrowingMetric()
        { }

        public bool IsEnabled()
            => throw new NotImplementedException();

        public void Log<T>(T value)
            => throw new NotImplementedException();

        public void Log<T, TTags>(T value, TTags? tags = null)
            where TTags : class
            => throw new NotImplementedException();
    }
}
