using System.Linq;
using Finite.Metrics;
using Finite.Metrics.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Finite.Metrics.Configuration.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="MetricsBuilderConfigurationExtensions"/>
    /// </summary>
    public class MetricsBuilderConfigurationExtensionsTests
    {
        /// <summary>
        /// Ensures that <see cref="MetricsBuilderConfigurationExtensions.AddConfiguration(IMetricsBuilder)"/>
        /// adds the required configuration services to
        /// <see cref="IMetricsBuilder.Services"/>.
        /// </summary>
        [Test]
        public void AddConfigurationAddsConfigurationServices()
        {
            var services = new ServiceCollection();
            var builder = new MetricsBuilder(services);

            _ = builder.AddConfiguration();

            var hasProviderConfigFactory = services.Any(
                x => x.ServiceType ==
                    typeof(IMetricProviderConfigurationFactory));
            var hasProviderConfiguration = services.Any(
                x => x.ServiceType == typeof(IMetricProviderConfiguration<>));

            Assert.True(hasProviderConfigFactory);
            Assert.True(hasProviderConfiguration);
        }

         /// <summary>
        /// Ensures that <see cref="MetricsBuilderConfigurationExtensions.AddConfiguration(IMetricsBuilder,IConfiguration)"/>
        /// adds a <see cref="MetricsConfiguration"/> instance, whose
        /// <see cref="MetricsConfiguration.Configuration"/> property equals
        /// the configuration we passed to <c>AddConfiguration</c>.
        /// </summary>
        [Test]
        public void AddConfigurationWithConfigAddsConfigurationServices()
        {
            var services = new ServiceCollection();
            var builder = new MetricsBuilder(services);
            var configuration = new ConfigurationBuilder().Build();

            _ = builder.AddConfiguration(configuration);

            var metricsConfigurationDescriptor = services.SingleOrDefault(
                x => x.ServiceType == typeof(MetricsConfiguration));

            Assert.NotNull(metricsConfigurationDescriptor);
            Assert.NotNull(
                metricsConfigurationDescriptor!.ImplementationInstance);

            var metricsConfiguration = (MetricsConfiguration)
                metricsConfigurationDescriptor!.ImplementationInstance!;

            Assert.AreEqual(configuration, metricsConfiguration.Configuration);
        }
    }
}
