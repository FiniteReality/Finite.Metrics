using Microsoft.Extensions.DependencyInjection;

namespace Finite.Metrics
{
    internal class MetricsBuilder : IMetricsBuilder
    {
        public MetricsBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
