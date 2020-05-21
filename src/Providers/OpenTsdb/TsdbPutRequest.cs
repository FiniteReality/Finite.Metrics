using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Finite.Metrics.OpenTsdb
{
    internal struct TsdbPutRequest
    {
        [JsonPropertyName("metric")]
        public string Metric { get; set; }

        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }

        [JsonPropertyName("tags")]
        public IDictionary<string, string>? Tags { get; set; }
    }
}
