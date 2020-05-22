using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Finite.Metrics.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="MetricFactory"/>
    /// </summary>
    public class MetricFactoryTests
    {
        /// <summary>
        /// Ensures that
        /// <see cref="MetricFactory.Create(Action{IMetricsBuilder})"/>
        /// calls the delegate parameter passed.
        /// </summary>
        [Test]
        public void CreateCallsDelegate()
        {
            _ = MetricFactory.Create(_ => Assert.Pass());

            Assert.Fail();
        }

        /// <summary>
        /// Ensures that
        /// <see cref="MetricFactory.Create(Action{IMetricsBuilder})"/>
        /// returns an instance of <see cref="IMetricFactory"/>.
        /// </summary>
        [Test]
        public void CreateReturnsNonNull()
            => Assert.NotNull(MetricFactory.Create(_ => { }));

        /// <summary>
        /// Ensures that <see cref="MetricFactory(System.Collections.Generic.IEnumerable{IMetricProvider})"/>
        /// populates the internal list of providers with the given enumerable.
        /// </summary>
        [Test]
        public void ConstructorPopulatesInternalProviderList()
        {
            var factory = new MetricFactory(new[]{
                new ThrowingMetricProvider()
            });

            Assert.IsNotEmpty(factory._providers);
        }

        /// <summary>
        /// Ensures that
        /// <see cref="MetricFactory.AddProvider(IMetricProvider)"/> adds a
        /// provider to the internal list of providers.
        /// </summary>
        [Test]
        public void AddProviderPopulatesInternalProviderList()
        {
            var factory = new MetricFactory(Array.Empty<IMetricProvider>());

            factory.AddProvider(new ThrowingMetricProvider());

            Assert.IsNotEmpty(factory._providers);
        }

        /// <summary>
        /// Ensures that <see cref="MetricFactory.Dispose"/> swallows
        /// exceptions from providers which erroneously throw on Dispose.
        /// </summary>
        [Test]
        public void DisposeSwallowsExceptions()
        {
            var factory = new MetricFactory(new[]{
                new ThrowingMetricProvider()
            });

            Assert.DoesNotThrow(() => factory.Dispose());
        }

        /// <summary>
        /// Ensures that <see cref="MetricFactory.Dispose"/> can safely be
        /// called multiple times.
        /// </summary>
        [Test]
        public void DisposeCanBeCalledMultipleTimes()
        {
            var factory = new MetricFactory(Array.Empty<IMetricProvider>());

            factory.Dispose();

            Assert.DoesNotThrow(() => factory.Dispose());
        }

        /// <summary>
        /// Ensures that <see cref="MetricFactory.CreateMetric(string)"/>
        /// returns an instance of <see cref="IMetric"/>.
        /// </summary>
        [Test]
        public void CreateMetricReturnsNonNull()
        {
            var factory = new MetricFactory(Array.Empty<IMetricProvider>());

            Assert.NotNull(factory.CreateMetric("A name"));
        }
    }
}
