using System;

namespace Finite.Metrics.UnitTests
{
    internal class NonThrowingMetricProvider : IMetricProvider
    {
        public IMetric CreateMetric(string metricName)
            => new EnabledNonThrowingMetric();

        public void Dispose()
        { }
    }
}
