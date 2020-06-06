using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Finite.Metrics.OpenTsdb
{
    internal class TsdbMetricsUploader : BackgroundService, IAsyncDisposable
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly ILogger _logger;
        private readonly IOptionsMonitor<OpenTsdbMetricsOptions> _options;
        private readonly ISystemClock _systemClock;

        private readonly bool _hostFreeMode;

        private ConcurrentBag<TsdbPutRequest> _lastLogs;
        private ConcurrentBag<TsdbPutRequest> _logs;

        public TsdbMetricsUploader(IHttpClientFactory clientFactory,
            ILogger<TsdbMetricsUploader> logger,
            IOptionsMonitor<OpenTsdbMetricsOptions> options,
            ISystemClock systemClock,
            IHost? host = null)
            : base()
        {
            _clientFactory = clientFactory;
            _logger = logger;
            _options = options;
            _systemClock = systemClock;

            _logs = new ConcurrentBag<TsdbPutRequest>();
            _lastLogs = new ConcurrentBag<TsdbPutRequest>();

            _hostFreeMode = host is null;

            if (_hostFreeMode)
            {
                _ = StartAsync(CancellationToken.None);
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_hostFreeMode)
                return;

            try
            {
                await StopAsync(CancellationToken.None);
            }
            catch (OperationCanceledException)
            { /* no-op */ }
        }

        public void AddLogEntry(TsdbPutRequest request)
        {
            request.Timestamp = _systemClock.UtcNow.ToUnixTimeMilliseconds();

            var defaultTags = _options.CurrentValue.DefaultTags;

            foreach (var pair in defaultTags)
            {
                _ = request.Tags.TryAdd(pair.Key, pair.Value);
            }

            if (!request.Tags.Any())
                throw new InvalidOperationException(
                    "Cannot upload a metric with no tags");

            _logs.Add(request);
        }

        protected override async Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            while (true)
            {
                var options = _options.CurrentValue;
                await Task.Delay(options.Interval, stoppingToken);

                using var client = _clientFactory.CreateClient(
                    OpenTsdbMetricsOptions.HttpClientName);

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
