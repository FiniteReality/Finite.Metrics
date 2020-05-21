using Microsoft.Extensions.Configuration;

namespace Finite.Metrics.Configuration
{
    internal class MetricsConfiguration
    {
        public IConfiguration Configuration { get; }

        public MetricsConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }
    }
}
