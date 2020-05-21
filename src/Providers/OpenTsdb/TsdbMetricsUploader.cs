using System.Collections.Concurrent;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.OpenTsdb
{
    internal class TsdbMetricsUploader : BackgroundService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<TsdbMetricsOptions> _options;

        private ConcurrentBag<TsdbPutRequest> _lastLogs;
        private ConcurrentBag<TsdbPutRequest> _logs;

        public TsdbMetricsUploader(IHttpClientFactory clientFactory,
            ILogger<TsdbMetricsUploader> logger,
            IOptionsMonitor<TsdbMetricsOptions> options)
            : base()
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _options = options;

            _logs = new ConcurrentBag<TsdbPutRequest>();
            _lastLogs = new ConcurrentBag<TsdbPutRequest>();
        }

        public void AddLogEntry(TsdbPutRequest request)
            => _logs.Add(request);

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (true)
            {
                stoppingToken.ThrowIfCancellationRequested();
                var options = _options.CurrentValue;
                using var client = _clientFactory.CreateClient(
                    TsdbMetricsOptions.HttpClientName);

                var logs = Interlocked.Exchange(ref _logs, _lastLogs);

                var response = await client.PostAsJsonAsync(
                    options.UploadMetricsEndpoint,
                    value: logs.ToArray(),
                    cancellationToken: stoppingToken);

                if (response.StatusCode != HttpStatusCode.NoContent)
                {
                    var errors = await response.Content!
                        .ReadFromJsonAsync<TsdbPutResponse>(
                            cancellationToken: stoppingToken);

                    _logger.LogWarning(
                        "Some metrics failed to upload: {success} " +
                        "successful, {failed} failed.",
                        errors.Successful,
                        errors.Failed);
                }

                logs.Clear();
                _lastLogs = logs;
            }
        }
    }
}
