using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Finite.Metrics.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="MetricsServiceCollectionExtensions"/>
    /// </summary>
    public class MetricsServiceCollectionExtensionsTests
    {
        /// <summary>
        /// Ensures that <see cref="MetricsServiceCollectionExtensions.AddMetrics(IServiceCollection, Action{IMetricsBuilder})"/>
        /// throws an instance of <see cref="ArgumentNullException"/> when
        /// <c>null</c> is passed as a parameter, and that the exception's
        /// <see cref="ArgumentException.ParamName"/> property was the expected
        /// parameter name.
        /// </summary>
        [Test]
        public void AddMetricsThrowsForNullServiceCollection()
        {
            var method = typeof(MetricsServiceCollectionExtensions)
                .GetMethod("AddMetrics", new[]
                {
                    typeof(IServiceCollection)
                })!;
            var parameter = method.GetParameters().First();

            var ex = Assert.Throws<ArgumentNullException>(
                () => MetricsServiceCollectionExtensions.AddMetrics(null!));

            Assert.AreEqual(parameter.Name, ex.ParamName);
        }

        /// <summary>
        /// Ensures the correct metrics services have been added to the service
        /// collection when using the <see cref="MetricsServiceCollectionExtensions.AddMetrics(IServiceCollection)"/>
        /// method.
        /// </summary>
        [Test]
        public void MetricsServicesAreAddedToCollection()
        {
            var collection = new ServiceCollection();

            _ = collection.AddMetrics();

            var hasIMetricFactory = collection.Any(
                x => x.ServiceType == typeof(IMetricFactory));
            var hasIMetric = collection.Any(
                x => x.ServiceType == typeof(IMetric));

            Assert.True(hasIMetricFactory);
            Assert.True(hasIMetric);
        }

        /// <summary>
        /// Ensures that the <see cref="MetricsServiceCollectionExtensions.AddMetrics(IServiceCollection)"/>
        /// method can be called multiple times without throwing exceptions.
        /// </summary>
        [Test]
        public void MetricsServicesCanBeAddedMultipleTimes()
        {
            var collection = new ServiceCollection();

            _ = collection.AddMetrics();

            Assert.DoesNotThrow(() => collection.AddMetrics());
        }

        /// <summary>
        /// Ensures that the <see cref="MetricsServiceCollectionExtensions.AddMetrics(IServiceCollection, System.Action{IMetricsBuilder})"/>
        /// method calls the passed delegate.
        /// </summary>
        [Test]
        public void AddMetricsWithDelegateCallsDelegate()
        {
            var collection = new ServiceCollection();

            _ = collection.AddMetrics(_ => Assert.Pass());

            Assert.Fail();
        }

        /// <summary>
        /// Ensures that the <see cref="MetricsServiceCollectionExtensions.AddMetrics(IServiceCollection, System.Action{IMetricsBuilder})"/>
        /// method calls the passed delegate with an instance of
        /// <see cref="IMetricsBuilder"/>.
        /// </summary>
        [Test]
        public void AddMetricsWithDelegatePassedIMetricsBuilder()
        {
            var collection = new ServiceCollection();

            _ = collection.AddMetrics(builder => Assert.NotNull(builder));
        }

        /// <summary>
        /// Ensures that the <see cref="MetricsServiceCollectionExtensions.AddMetrics(IServiceCollection, System.Action{IMetricsBuilder})"/>
        /// method calls the passed delegate with an instance of
        /// <see cref="IMetricsBuilder"/>, whose
        /// <see cref="IMetricsBuilder.Services"/> property equals the
        /// container we called <c>AddMetrics</c> on.
        /// </summary>
        [Test]
        public void AddMetricsWithDelegateHasSameContainer()
        {
            var collection = new ServiceCollection();

            _ = collection.AddMetrics(
                builder => Assert.AreEqual(collection, builder.Services));
        }
    }
}
