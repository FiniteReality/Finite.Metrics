using System;
using System.Net.Http;
using Finite.Metrics.Configuration;
using Finite.Metrics.OpenTsdb;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Finite.Metrics
{
    /// <summary>
    /// OpenTSDB extensions for <see cref="IMetricsBuilder"/>.
    /// </summary>
    public static class TsdbMetricsFactoryExtensions
    {
        /// <summary>
        /// Adds an OpenTSDB provider named 'OpenTSDB' to the factory.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMetricsBuilder"/> to use.
        /// </param>
        /// <param name="connectionString">
        /// The connection string to use when connecting to OpenTSDB.
        /// </param>
        /// <returns>
        /// The <paramref name="builder"/>, to allow for chaining.
        /// </returns>
        public static IMetricsBuilder AddOpenTsdb(this IMetricsBuilder builder,
            string connectionString)
        {
            var baseAddress = new Uri(connectionString, UriKind.Absolute);

            _ = builder.AddConfiguration();

            builder.Services.TryAddEnumerable(ServiceDescriptor
                .Singleton<IMetricProvider, TsdbMetricProvider>());

            MetricProviderOptions.RegisterProviderOptions
                <TsdbMetricsOptions, TsdbMetricProvider>(builder.Services);

            _ = builder.Services.AddHostedService<TsdbMetricsUploader>();

            _ = builder.Services.AddHttpClient(
                TsdbMetricsOptions.HttpClientName,
                (client) => client.BaseAddress = baseAddress);

            return builder;
        }

        /// <summary>
        /// Adds an OpenTSDB provider named 'OpenTSDB' to the factory.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMetricsBuilder"/> to use.
        /// </param>
        /// <param name="connectionString">
        /// The connection string to use when connecting to OpenTSDB.
        /// </param>
        /// <param name="configure">
        /// A delegate to congfigure the OpenTSDB provider.
        /// </param>
        /// <returns>
        /// The <paramref name="builder"/>, to allow for chaining.
        /// </returns>
        public static IMetricsBuilder AddOpenTsdb(this IMetricsBuilder builder,
            string connectionString,
            Action<TsdbMetricsOptions> configure)
        {
            _ = builder.AddOpenTsdb(connectionString);

            builder.Services.Configure(configure);

            return builder;
        }
    }
}
