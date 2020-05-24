using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace Finite.Metrics.Configuration
{
    internal class MetricProviderConfigurationFactory
        : IMetricProviderConfigurationFactory
    {
        private readonly IEnumerable<MetricsConfiguration> _configurations;

        public MetricProviderConfigurationFactory(
            IEnumerable<MetricsConfiguration> configurations)
        {
            _configurations = configurations;
        }

        public IConfiguration GetConfiguration(Type providerType)
        {
            if (providerType is null)
                throw new ArgumentNullException(nameof(providerType));

            var fullName = providerType.FullName;
            // TODO: provider alias support
            var configurationBuilder = new ConfigurationBuilder();

            foreach (var configuration in _configurations)
            {
                var sectionFromFullName = configuration.Configuration
                    .GetSection(fullName);

                _ = configurationBuilder.AddConfiguration(sectionFromFullName);
            }

            return configurationBuilder.Build();
        }
    }
}
