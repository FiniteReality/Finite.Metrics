using System;
using System.Collections.Generic;

namespace Finite.Metrics
{
    internal class Metric : IMetric
    {
        public IMetric[] Metrics { get; set; } = null!;

        public void Log<T>(T value, TagValues? tags = null)
        {
            var metrics = Metrics;

            if (metrics == null || metrics.Length == 0)
                return;

            List<Exception>? exceptions = null;
            for (var x = 0; x < metrics.Length; x++)
            {
                var metric = metrics[x];

                if (!metric.IsEnabled())
                    continue;

                try
                {
                    metric.Log(value, tags);
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                        exceptions = new List<Exception>();

                    exceptions.Add(ex);
                }
            }

            if (exceptions != null)
            {
                throw new AggregateException(
                    "An error occured while writing to metric(s):",
                    exceptions);
            }
        }

        public bool IsEnabled()
        {
            var metrics = Metrics;

            if (metrics == null || metrics.Length == 0)
                return false;

            List<Exception>? exceptions = null;
            for (var x = 0; x < metrics.Length; x++)
            {
                var metric = metrics[x];

                try
                {
                    if (metric.IsEnabled())
                        return true;
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                        exceptions = new List<Exception>();

                    exceptions.Add(ex);
                }
            }

            return exceptions != null
                ? throw new AggregateException(
                    "An error occured while writing to metric(s):",
                    exceptions)
                : false;
        }
    }
}
