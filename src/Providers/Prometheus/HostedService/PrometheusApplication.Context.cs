using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;

namespace Finite.Metrics.Prometheus.HostedService
{
    internal partial class PrometheusApplication
    {
        public class Context
        {
            private readonly IHttpRequestFeature _request;
            private readonly IHttpResponseFeature _response;
            private readonly IHttpResponseBodyFeature _responseBody;

            public Context(IHttpRequestFeature request,
                IHttpResponseFeature response,
                IHttpResponseBodyFeature responseBody)
            {
                _request = request;
                _response = response;
                _responseBody = responseBody;
            }

            public int StatusCode
            {
                get => _response.StatusCode;
                set => _response.StatusCode = value;
            }

            public string RequestPath
            {
                get => _request.Path;
                set => _request.Path = value;
            }

            public void Write(string value)
                => _ = Encoding.UTF8.GetBytes(value, _responseBody.Writer);

            public async Task CompleteAsync()
                => await _responseBody.CompleteAsync();
        }
    }
}
