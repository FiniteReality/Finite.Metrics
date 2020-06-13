using System;
using System.Collections.Generic;

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
        public TimeSpan Interval { get; set; } = TimeSpan.FromSeconds(5);

        /// <summary>
        /// Gets or sets the endpoint used to upload metrics to OpenTSDB.
        /// </summary>
        public string UploadMetricsEndpoint { get; set; } = "/api/put";

        /// <summary>
        /// Gets or sets the default tags to apply to metrics uploaded to
        /// OpenTSDB.
        /// </summary>
        public IDictionary<string, string> DefaultTags { get; set; }
            = new Dictionary<string, string>();
    }
}
