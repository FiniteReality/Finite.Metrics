using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Finite.Metrics.Prometheus.HostedService
{
    // A simple wrapper over the IServiceCollection present in IMetricsBuilder
    internal class WrapperWebHostBuilder : IWebHostBuilder
    {
        private readonly IServiceCollection _services;

        public WrapperWebHostBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public IWebHost Build()
            => throw new NotSupportedException();
        public IWebHostBuilder ConfigureAppConfiguration(
            Action<WebHostBuilderContext, IConfigurationBuilder> configureDelegate)
            => throw new NotSupportedException();

        public IWebHostBuilder ConfigureServices(
            Action<IServiceCollection> configureServices)
        {
            configureServices(_services);

            return this;
        }

        public IWebHostBuilder ConfigureServices(
            Action<WebHostBuilderContext, IServiceCollection> configureServices)
            => throw new NotSupportedException();

        public string GetSetting(string key)
            => throw new NotSupportedException();
        public IWebHostBuilder UseSetting(string key, string? value)
            => throw new NotSupportedException();
    }
}
