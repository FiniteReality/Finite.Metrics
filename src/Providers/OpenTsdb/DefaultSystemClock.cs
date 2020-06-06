using System;

namespace Finite.Metrics.OpenTsdb
{
    internal class DefaultSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
    }
}
