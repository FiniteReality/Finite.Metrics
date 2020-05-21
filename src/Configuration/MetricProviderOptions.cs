using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.Configuration
{
    /// <summary>
    /// Provides a set of helpers to initialize options objects from metrics
    /// provider configuration.
    /// </summary>
    public static class MetricProviderOptions
    {
        /// <summary>
        /// Indicates that settings for <typeparamref name="TProvider"/> should
        /// be loaded into <typeparamref name="TOptions"/>.
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to register options in.
        /// </param>
        /// <typeparam name="TOptions">
        /// The options class
        /// </typeparam>
        /// <typeparam name="TProvider">
        /// The provider class
        /// </typeparam>
        public static void RegisterProviderOptions<TOptions, TProvider>(
            IServiceCollection services)
            where TOptions : class
        {
            services.TryAddEnumerable(ServiceDescriptor.Singleton<
                IConfigureOptions<TOptions>,
                MetricProviderConfigureOptions<TOptions, TProvider>>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<
                IOptionsChangeTokenSource<TOptions>,
                MetricProviderOptionsChangeTokenSource<TOptions, TProvider>>()
            );
        }
    }
}
