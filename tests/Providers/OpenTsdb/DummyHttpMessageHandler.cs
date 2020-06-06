using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Finite.Metrics.OpenTsdb.UnitTests
{
    internal class DummyHttpMessageHandler : HttpMessageHandler
    {
        private TaskCompletionSource<HttpResponseMessage>? _mostRecentResponse;

        public HttpRequestMessage? MostRecentMessage { get; private set; }

        public DummyHttpMessageHandler()
        { }

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            MostRecentMessage = request;

            _mostRecentResponse =
                new TaskCompletionSource<HttpResponseMessage>();

            return _mostRecentResponse.Task;
        }

        public void RespondToMostRecentMessage(HttpResponseMessage message)
        {
            if (_mostRecentResponse is null)
                return;

            _ = _mostRecentResponse.TrySetResult(message);
        }
    }
}
