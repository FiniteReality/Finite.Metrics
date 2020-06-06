using System;

namespace Finite.Metrics.OpenTsdb.UnitTests
{
    internal class TestSystemClock : ISystemClock
    {
        public DateTimeOffset UtcNow => DateTimeOffset.UnixEpoch;
    }
}
