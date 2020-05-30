using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Finite.Metrics.Configuration.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="MetricProviderConfiguration{T}"/>
    /// </summary>
    public class MetricProviderConfigurationTests
    {
        /// <summary>
        /// Ensures that
        /// <see cref="MetricProviderConfiguration{T}.Configuration"/>
        /// returns a populated instance of <see cref="IConfiguration"/> when
        /// it has a backing configuration.
        /// </summary>
        [Test]
        public void MetricProviderConfigurationHasSameConfiguration()
        {
            const string ExpectedConfigurationKey = "MyProperty";
            const string ExpectedConfigurationValue = "OK";

            var inputConfiguration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    KeyValuePair.Create(
                        $"{typeof(ThrowingOptions).FullName}:{ExpectedConfigurationKey}",
                        ExpectedConfigurationValue)
                })
                .Build();

            var factory = new MetricProviderConfigurationFactory(
                new[]
                {
                    new MetricsConfiguration(inputConfiguration)
                });

            var outputConfiguration = factory.GetConfiguration(
                typeof(ThrowingOptions));
            var value = outputConfiguration[ExpectedConfigurationKey];

            var outputConfiguration2 =
                new MetricProviderConfiguration<ThrowingOptions>(factory)
                    .Configuration;

            Assert.NotNull(outputConfiguration2);

            var value2 = outputConfiguration2[ExpectedConfigurationKey];

            Assert.AreEqual(value, value2);
        }
    }
}
