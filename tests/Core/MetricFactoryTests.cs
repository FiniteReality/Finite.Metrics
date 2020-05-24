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
        /// Ensures that
        /// <see cref="MetricFactory.Create(Action{IMetricsBuilder})"/>
        /// returns an instance of <see cref="IMetricFactory"/>, whose methods
        /// do not throw.
        /// </summary>
        [Test]
        public void CreateReturnsSafeFactory()
        {
            var factory = MetricFactory.Create(_ => { });

            Assert.DoesNotThrow(
                () => factory.AddProvider(new NonThrowingMetricProvider()));
            Assert.DoesNotThrow(
                () => factory.CreateMetric("test"));
            Assert.DoesNotThrow(
                () => factory.Dispose());
        }

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
        /// <see cref="MetricFactory.AddProvider(IMetricProvider)"/> throws an
        /// throws an instance of <see cref="ArgumentNullException"/> when
        /// <c>null</c> is passed as a parameter, and that the exception's
        /// <see cref="ArgumentException.ParamName"/> property was the expected
        /// parameter name.
        /// </summary>
        [Test]
        public void AddProviderPopulatesInternalProviderList()
        {
            var factory = new MetricFactory(Array.Empty<IMetricProvider>());

            var method = factory.GetType().GetMethod("AddProvider")!;
            var parameter = method.GetParameters().First();

            var ex = Assert.Throws<ArgumentNullException>(
                () => factory.AddProvider(null!));

            Assert.AreEqual(parameter.Name, ex.ParamName);
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
            var factory = new MetricFactory(new IMetricProvider[]{
                new NonThrowingMetricProvider(),
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

        /// <summary>
        /// Ensures that <see cref="MetricFactory.CreateMetric(string)"/>
        /// returns an instance of <see cref="Metric"/> whose
        /// <see cref="Metric.Metrics"/> property is non-empty.
        /// </summary>
        [Test]
        public void CreateMetricReturnsNonEmptyMetric()
        {
            var factory = new MetricFactory(new[]
            {
                new NonThrowingMetricProvider()
            });

            var metric = factory.CreateMetric("A name") as Metric;

            Assert.NotNull(metric);
            Assert.IsNotEmpty(metric!.Metrics);
        }
    }
}
