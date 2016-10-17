using System.Diagnostics;
using System.Net.Http;

namespace MessageHandlerSelfHostConfiguration.MessageHandlers {
    public class CustomMessageHandler1 : DelegatingHandler {
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
                                                                                      System.Threading.CancellationToken
                                                                                          cancellationToken) {
            Debug.WriteLine("CustomMessageHandler1 request invoked");
            return base.SendAsync(request, cancellationToken).ContinueWith((task) => {
                                                                               Debug.WriteLine(
                                                                                   "CustomMessageHandler1 response invoked");
                                                                               var response = task.Result;
                                                                               return response;
                                                                           }
                );
        }
    }
}