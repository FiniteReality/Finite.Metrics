using System;

namespace Finite.Metrics.UnitTests
{
    internal class ThrowingMetricProvider : IMetricProvider
    {
        public IMetric CreateMetric(string metricName)
            => throw new NotImplementedException();

        public void Dispose()
            => throw new NotImplementedException();
    }
}
