using System;
using NUnit.Framework;

namespace Finite.Metrics.Configuration.UnitTests
{
    /// <summary>
    /// Unit tests for
    /// <see cref="MetricProviderConfigureOptions{TOptions,TProvider}"/>
    /// </summary>
    public class MetricProviderConfigureOptionsTests
    {
        /// <summary>
        /// Ensures that
        /// <see cref="MetricProviderConfigureOptions{TOptions,TProvider}"/>
        /// can be constructed.
        /// </summary>
        [Test]
        public void MetricProviderConfigureOptionsCanBeConstructed()
        {
            Assert.DoesNotThrow(() =>
            {
                var factory = new MetricProviderConfigurationFactory(
                    Array.Empty<MetricsConfiguration>());
                var configuration = new MetricProviderConfiguration<
                    ThrowingProvider>(factory);
                var options = new MetricProviderConfigureOptions<
                    ThrowingOptions, ThrowingProvider>(configuration);
            });
        }

    }
}
