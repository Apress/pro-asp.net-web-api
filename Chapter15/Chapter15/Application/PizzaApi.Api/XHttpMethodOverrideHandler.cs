using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PizzaApi.Api
{
    public class XHttpMethodOverrideHandler : DelegatingHandler
    {
        readonly string[] _methods = { "DELETE", "HEAD", "PUT" };
        public const string XOVERRIDEHEADER = "X-HTTP-Method-Override";

        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Check for HTTP POST with the X-HTTP-Method-Override header.
            if (HttpMethod.Post == request.Method && request.Headers.Contains(XOVERRIDEHEADER))
            {
                // Check if the header value is in our methods list.
                var overrideMethod = request.Headers.GetValues(XOVERRIDEHEADER).FirstOrDefault();
                if (_methods.Contains(overrideMethod, StringComparer.InvariantCultureIgnoreCase))
                {
                    // Change the request method.
                    request.Method = new HttpMethod(overrideMethod);
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }

}