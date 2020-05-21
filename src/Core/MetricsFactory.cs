using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Finite.Metrics
{
    /// <summary>
    /// Produces instances of <see cref="IMetric"/> classes based on the given
    /// providers.
    /// </summary>
    public class MetricsFactory : IMetricsFactory
    {
        private readonly List<IMetricProvider> _providers;

        /// <summary>
        /// Creates a new <see cref="MetricsFactory"/> instance.
        /// </summary>
        /// <param name="providers">
        /// The providers to use in producing <see cref="IMetric"/> instances.
        /// </param>
        public MetricsFactory(IEnumerable<IMetricProvider> providers)
        {
            _providers = providers.ToList();
        }

        /// <summary>
        /// Creates a new instance of <see cref="IMetricsFactory"/> configured
        /// using the provided <paramref name="configure"/> delegate.
        /// </summary>
        /// <param name="configure">
        /// A delegate to configure the <see cref="IMetricsBuilder"/>.
        /// </param>
        /// <returns>
        /// The <see cref="IMetricsFactory"/> that was created.
        /// </returns>
        public static IMetricsFactory Create(Action<IMetricsBuilder> configure)
        {
            var collection = new ServiceCollection();
            _ = collection.AddMetrics(configure);
            var provider = collection.BuildServiceProvider();

            var factory = provider.GetService<IMetricsFactory>();

            return new DisposingMetricsFactory(factory, provider);
        }

        /// <inheritdoc/>
        public void AddProvider(IMetricProvider provider)
            => _providers.Add(provider);

        /// <inheritdoc/>
        public IMetric CreateMetric(string metricName)
        {
            var metrics = new IMetric[_providers.Count];
            for (var i = 0; i < _providers.Count; i++)
                metrics[i] = _providers[i].CreateMetric(metricName);

            return new Metric()
            {
                Metrics = metrics
            };
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            foreach (var provider in _providers)
            {
                try
                {
                    provider.Dispose();
                }
                catch
                {
                    // swallow exceptions on dispose
                }
            }
        }

        private class DisposingMetricsFactory : IMetricsFactory
        {
            private readonly IMetricsFactory _factory;

            private readonly ServiceProvider _services;

            public DisposingMetricsFactory(IMetricsFactory factory,
                ServiceProvider services)
            {
                _factory = factory;
                _services = services;
            }

            public void AddProvider(IMetricProvider provider)
                => _factory.AddProvider(provider);

            public IMetric CreateMetric(string metricName)
                => _factory.CreateMetric(metricName);

            public void Dispose()
                => _services.Dispose();
        }
    }
}
