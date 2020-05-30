using System;
using NUnit.Framework;

namespace Finite.Metrics.Configuration.UnitTests
{
    /// <summary>
    /// Unit tests for
    /// <see cref="MetricProviderOptionsChangeTokenSource{TOptions,TProvider}"/>
    /// </summary>
    public class MetricProviderOptionsChangeTokenSourceTests
    {
        /// <summary>
        /// Ensures that
        /// <see cref="MetricProviderOptionsChangeTokenSource{TOptions,TProvider}"/>
        /// can be constructed.
        /// </summary>
        [Test]
        public void MetricProviderOptionsChangeTokenSourceCanBeConstructed()
        {
            Assert.DoesNotThrow(() =>
            {
                var factory = new MetricProviderConfigurationFactory(
                    Array.Empty<MetricsConfiguration>());
                var configuration = new MetricProviderConfiguration<
                    ThrowingProvider>(factory);
                var cts = new MetricProviderOptionsChangeTokenSource<
                    ThrowingOptions, ThrowingProvider>(configuration);
            });
        }

    }
}
