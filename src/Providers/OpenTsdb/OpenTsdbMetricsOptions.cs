using System;

namespace Finite.Metrics.OpenTsdb
{
    /// <summary>
    /// Options used by the OpenTSDB metrics provider.
    /// </summary>
    public class OpenTsdbMetricsOptions
    {
        /// <summary>
        /// Gets the name of the HTTP client used by OpenTSDB.
        /// </summary>
        public static string HttpClientName
            => "OpenTSDB";

        /// <summary>
        /// Gets or sets the interval to wait for between uploading metrics to
        /// OpenTSDB.
        /// </summary>
        public TimeSpan Interval { get; set; }

        /// <summary>
        /// Gets or sets the endpoint used to upload metrics to OpenTSDB.
        /// </summary>
        public string UploadMetricsEndpoint { get; set; } = "/api/put";
    }
}
