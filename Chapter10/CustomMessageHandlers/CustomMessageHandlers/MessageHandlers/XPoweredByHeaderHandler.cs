using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CustomMessageHandlers.MessageHandlers {
    public class XPoweredByHeaderHandler : DelegatingHandler {
        private const string XPOWEREDBYHEADER = "X-Powered-By";
        private const string XPOWEREDBYVALUE = "ASP.NET Web API";

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken) {
            return base.SendAsync(request, cancellationToken).ContinueWith(
                (task) => {
                    HttpResponseMessage response = task.Result;
                    response.Headers.Add(XPOWEREDBYHEADER, XPOWEREDBYVALUE);
                    return response;
                }
                );
        }
    }
}