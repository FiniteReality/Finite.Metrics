using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace Finite.Metrics.OpenTsdb.UnitTests
{
    /// <summary>
    /// End-to-end tests for the OpenTSDB metrics provider.
    /// </summary>
    public class OpenTsdbProviderTests
    {
        private DummyHttpMessageHandler _httpHandler = null!;
        private IMetricFactory _factory = null!;

        /// <summary>
        /// Setup for end-to-end test
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _httpHandler = new DummyHttpMessageHandler();

            _factory = MetricFactory.Create(builder =>
            {
                _ = builder.Services.AddSingleton
                    <ISystemClock, TestSystemClock>();

                _ = builder.Services.AddHttpClient(
                    OpenTsdbMetricsOptions.HttpClientName)
                    .ConfigurePrimaryHttpMessageHandler(
                        () => _httpHandler);

                _ = builder.AddOpenTsdb("http://localhost/",
                    options =>
                    {
                        options.Interval = TimeSpan.FromSeconds(0.5);

                        options.DefaultTags["Latency"] = "0.5";
                    });
            });
        }

        /// <summary>
        /// Teardown from end-to-end test
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            _factory.Dispose();
            _httpHandler.Dispose();
        }

        /// <summary>
        /// Ensures that logging a metric with tags correctly grabs the tags.
        /// </summary>
        [Test]
        public async Task LogWithTagsAsync()
        {
            var metric = _factory.CreateMetric("endtoend");
            metric.Log(1, new { HostName = "woah" });

            await Task.Delay(TimeSpan.FromSeconds(1.5));

            Assert.NotNull(_httpHandler.MostRecentMessage);

            var request = _httpHandler.MostRecentMessage!;

            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.IsInstanceOf<JsonContent>(request.Content);

            var content = (JsonContent)request.Content!;
            var text = await content.ReadAsStringAsync();

            // TODO: this should be fuzzy; HostName and Latency may be swapped
            Assert.AreEqual(
                @"[{""metric"":""endtoend"",""timestamp"":0,""value"":1,""tags"":{""HostName"":""woah"",""Latency"":""0.5""}}]",
                text);
        }

        /// <summary>
        /// Ensures that logging a metric without metrics correctly applies the
        /// default tags.
        /// </summary>
        [Test]
        public async Task LogWithoutTagsAsync()
        {
            var metric = _factory.CreateMetric("endtoend");
            metric.Log(1);

            await Task.Delay(TimeSpan.FromSeconds(1.5));

            Assert.NotNull(_httpHandler.MostRecentMessage);

            var request = _httpHandler.MostRecentMessage!;

            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.IsInstanceOf<JsonContent>(request.Content);

            var content = (JsonContent)request.Content!;
            var text = await content.ReadAsStringAsync();

            Assert.AreEqual(
                @"[{""metric"":""endtoend"",""timestamp"":0,""value"":1,""tags"":{""Latency"":""0.5""}}]",
                text);
        }
    }
}
