using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace Finite.Metrics.Configuration.UnitTests
{
    /// <summary>
    /// Unit tests for <see cref="MetricProviderOptions"/>
    /// </summary>
    public class MetricProviderOptionsTests
    {
        /// <summary>
        /// Ensures that <see cref="MetricProviderOptions.RegisterProviderOptions{TOptions, TProvider}(Microsoft.Extensions.DependencyInjection.IServiceCollection)"/>
        /// adds the required services to the service collection.
        /// </summary>
        [Test]
        public static void RegisterProviderOptionsAddsRequiredServices()
        {
            var services = new ServiceCollection();

            MetricProviderOptions
                .RegisterProviderOptions<ThrowingOptions, ThrowingProvider>(
                    services);

            var hasConfigureOptions = services.Any(
                x => x.ServiceType == typeof(
                    IConfigureOptions<ThrowingOptions>));
            var hasChangeTokenSource = services.Any(
                x => x.ServiceType == typeof(
                    IOptionsChangeTokenSource<ThrowingOptions>));

            Assert.True(hasConfigureOptions);
            Assert.True(hasChangeTokenSource);
        }
    }
}
