using System;
using System.Linq;
using System.Net.Http;
using Finite.Metrics.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace Finite.Metrics.OpenTsdb.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="MetricsBuilderOpenTsdbExtensions"/>
    /// </summary>
    public class MetricsBuilderTsdbExtensionsTests
    {
        /// <summary>
        /// Ensures that <see cref="MetricsBuilderOpenTsdbExtensions.AddOpenTsdb(IMetricsBuilder, string)"/>
        /// throws an instance of <see cref="UriFormatException"/> when an
        /// invalid connection string has been passed, and also ensures that it
        /// throws an instance of <see cref="ArgumentNullException"/> when
        /// <c>null</c> is passed.
        /// </summary>
        [Test]
        public void AddOpenTsdbInvalidConnectionStringThrows()
        {
            var services = new ServiceCollection();
            var builder = new MetricsBuilder(services);

            _ = Assert.Throws<UriFormatException>(
                () => _ = builder.AddOpenTsdb("invalid uri text"));

            _ = Assert.Throws<ArgumentNullException>(
                () => _ = builder.AddOpenTsdb(null!));
        }

        /// <summary>
        /// Ensures that <see cref="MetricsBuilderOpenTsdbExtensions.AddOpenTsdb(IMetricsBuilder, string)"/>
        /// adds the required OpenTSDB services to
        /// <see cref="IMetricsBuilder.Services"/>.
        /// </summary>
        [Test]
        public void AddOpenTsdbAddsTsdbServices()
        {
            var services = new ServiceCollection();
            var builder = new MetricsBuilder(services);

            _ = builder.AddOpenTsdb("http://localhost/");

            var hasConfiguration = services.Any(
                x => x.ServiceType
                    == typeof(IMetricProviderConfigurationFactory));
            var hasMetricsUploader = services.Any(
                x => x.ImplementationType == typeof(TsdbMetricsUploader));
            var hasMetricProvider = services.Any(
                x => x.ImplementationType == typeof(TsdbMetricProvider));
            var hasHostedService = services.Any(
                x => x.ServiceType == typeof(IHostedService));
            var hasHttpClientFactory = services.Any(
                x => x.ServiceType == typeof(IHttpClientFactory));

            Assert.True(hasConfiguration);
            Assert.True(hasMetricsUploader);
            Assert.True(hasMetricProvider);
            Assert.True(hasHostedService);
            Assert.True(hasHttpClientFactory);
        }

        /// <summary>
        /// Ensures that <see cref="MetricsBuilderOpenTsdbExtensions.AddOpenTsdb(IMetricsBuilder, string, Action{OpenTsdbMetricsOptions})"/>
        /// throws an instance of <see cref="UriFormatException"/> when an
        /// invalid connection string has been passed, and also ensures that it
        /// throws an instance of <see cref="ArgumentNullException"/> when
        /// <c>null</c> is passed.
        /// </summary>
        [Test]
        public void AddOpenTsdbConfigurationInvalidConnectionStringThrows()
        {
            var services = new ServiceCollection();
            var builder = new MetricsBuilder(services);

            _ = Assert.Throws<UriFormatException>(
                () => _ = builder.AddOpenTsdb("invalid uri text",
                    options => { }));

            _ = Assert.Throws<ArgumentNullException>(
                () => _ = builder.AddOpenTsdb(null!));
        }

        /// <summary>
        /// Ensures that <see cref="MetricsBuilderOpenTsdbExtensions.AddOpenTsdb(IMetricsBuilder, string, Action{OpenTsdbMetricsOptions})"/>
        /// adds the required OpenTSDB services to
        /// <see cref="IMetricsBuilder.Services"/>.
        /// </summary>
        [Test]
        public void AddOpenTsdbConfigurationAddsTsdbServices()
        {
            var services = new ServiceCollection();
            var builder = new MetricsBuilder(services);

            _ = builder.AddOpenTsdb("http://localhost/",
                options => { });

            var hasConfiguration = services.Any(
                x => x.ServiceType
                    == typeof(IMetricProviderConfigurationFactory));
            var hasMetricsUploader = services.Any(
                x => x.ImplementationType == typeof(TsdbMetricsUploader));
            var hasMetricProvider = services.Any(
                x => x.ImplementationType == typeof(TsdbMetricProvider));
            var hasHostedService = services.Any(
                x => x.ServiceType == typeof(IHostedService));
            var hasHttpClientFactory = services.Any(
                x => x.ServiceType == typeof(IHttpClientFactory));

            Assert.True(hasConfiguration);
            Assert.True(hasMetricsUploader);
            Assert.True(hasMetricProvider);
            Assert.True(hasHostedService);
            Assert.True(hasHttpClientFactory);
        }
    }
}
