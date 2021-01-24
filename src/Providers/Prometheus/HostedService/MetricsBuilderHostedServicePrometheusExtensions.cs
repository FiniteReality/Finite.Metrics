using System;
using Finite.Metrics.Configuration;
using Finite.Metrics.Prometheus;
using Finite.Metrics.Prometheus.HostedService;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Finite.Metrics
{
    /// <summary>
    /// Prometheus extensions for <see cref="IMetricsBuilder"/>.
    /// </summary>
    public static class MetricsBuilderHostedServicePrometheusExtensions
    {
        /// <summary>
        /// Adds Prometheus services to the specified
        /// <see cref="IMetricsBuilder"/>.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMetricsBuilder"/> to add services to.
        /// </param>
        /// <returns>
        /// The <paramref name="builder"/>, to allow for chaining.
        /// </returns>
        public static IMetricsBuilder AddPrometheus(
            this IMetricsBuilder builder)
        {
            _ = builder.AddPrometheusCore();

#pragma warning disable CS0618
            builder.Services.TryAddSingleton<IApplicationLifetime, PrometheusApplicationLifetime>();
#pragma warning restore CS0618

            _ = new WrapperWebHostBuilder(builder.Services)
                .UseKestrel();

            _ = builder.Services.AddHostedService<PrometheusMetricsHostedService>();

            return builder;
        }

        /// <summary>
        /// Adds Prometheus services to the specified
        /// <see cref="IMetricsBuilder"/>.
        /// </summary>
        /// <param name="builder">
        /// The <see cref="IMetricsBuilder"/> to add services to.
        /// </param>
        /// <param name="configure">
        /// A delegate to configure the Prometheus provider.
        /// </param>
        /// <returns>
        /// The <paramref name="builder"/>, to allow for chaining.
        /// </returns>
        public static IMetricsBuilder AddPrometheus(
            this IMetricsBuilder builder, Action<PrometheusOptions> configure)
        {
            _ = builder.AddPrometheus();
            _ = builder.Services.Configure(configure);
            _ = builder.Services.ConfigureOptions<KestrelConfigureOptions>();

            return builder;
        }
    }
}
