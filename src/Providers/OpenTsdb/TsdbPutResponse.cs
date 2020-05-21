using System.Text.Json.Serialization;

namespace Finite.Metrics.OpenTsdb
{
    internal struct TsdbPutResponse
    {
        [JsonPropertyName("success")]
        public int Successful { get; set; }

        [JsonPropertyName("failed")]
        public int Failed { get; set; }
    }
}
