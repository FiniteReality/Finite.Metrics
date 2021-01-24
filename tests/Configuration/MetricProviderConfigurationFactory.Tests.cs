using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Finite.Metrics.Configuration.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="MetricProviderConfigurationFactory"/>
    /// </summary>
    public class MetricProviderConfigurationFactoryTests
    {
        /// <summary>
        /// Ensures that <see cref="MetricProviderConfigurationFactory.GetConfiguration(Type)"/>
        /// throws an instance of <see cref="ArgumentNullException"/> when
        /// <c>null</c> is passed as a parameter, and that the exception's
        /// <see cref="ArgumentException.ParamName"/> property was the expected
        /// parameter name.
        /// </summary>
        [Test]
        public void GetConfigurationThrowsArgumentNullException()
        {
            var factory = new MetricProviderConfigurationFactory(
                Array.Empty<MetricsConfiguration>());

            var method = factory.GetType().GetMethod("GetConfiguration")!;
            var parameter = method.GetParameters().First();

            var ex = Assert.Throws<ArgumentNullException>(
                () => factory.GetConfiguration(null!))!;

            Assert.AreEqual(parameter.Name, ex.ParamName);
        }

        /// <summary>
        /// Ensures that <see cref="MetricProviderConfigurationFactory.GetConfiguration(Type)"/>
        /// returns an instance of <see cref="IConfiguration"/>.
        /// </summary>
        [Test]
        public void GetConfigurationReturnsNonNull()
        {
            var factory = new MetricProviderConfigurationFactory(
                Array.Empty<MetricsConfiguration>());

            var config = factory.GetConfiguration(typeof(ThrowingOptions));

            Assert.NotNull(config);
        }

        /// <summary>
        /// Ensures that <see cref="MetricProviderConfigurationFactory.GetConfiguration(Type)"/>
        /// returns an empty instance of <see cref="IConfiguration"/> when it
        /// has no backing configuration providers.
        /// </summary>
        [Test]
        public void GetConfigurationWithNoConfigurationReturnsEmpty()
        {
            var factory = new MetricProviderConfigurationFactory(
                Array.Empty<MetricsConfiguration>());

            var config = factory.GetConfiguration(typeof(ThrowingOptions));

            Assert.IsEmpty(config.AsEnumerable());
        }

        /// <summary>
        /// Ensures that <see cref="MetricProviderConfigurationFactory.GetConfiguration(Type)"/>
        /// returns a populated instance of <see cref="IConfiguration"/> when
        /// it has a backing configuration.
        /// </summary>
        [Test]
        public void GetConfigurationWithConfigurationReturnsNonEmpty()
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

            Assert.AreEqual(ExpectedConfigurationValue, value);
        }
    }
}
