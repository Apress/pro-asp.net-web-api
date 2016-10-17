using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CustomMessageHandlers.MessageHandlers {
    public class ApiKeyProtectionMessageHandler : DelegatingHandler {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                               CancellationToken cancellationToken) {
            IEnumerable<string> values;
            request.Headers.TryGetValues("apikey", out values);
            if (null != values && values.Count() == 1) {
                return base.SendAsync(request, cancellationToken);
            }

            var tcs = new TaskCompletionSource<HttpResponseMessage>();
            tcs.SetResult(new HttpResponseMessage(HttpStatusCode.Unauthorized) {
                                                                                   ReasonPhrase = "API Key required."
                                                                               });
            return tcs.Task;
        }
    }
}