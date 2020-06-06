using System;

namespace Finite.Metrics.OpenTsdb
{
    internal interface ISystemClock
    {
        DateTimeOffset UtcNow { get; }
    }
}
