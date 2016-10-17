using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace PizzaApi.Api.Tests
{
    public class XHttpMethodOverrideHandlerTests
    {
        [Theory]
        [InlineData("POST", "PUT", "PUT")]
        [InlineData("POST", "DELETE", "DELETE")]
        [InlineData("POST", "GET", "POST")]
        [InlineData("POST", "", "POST")]
        [InlineData("POST", "HEAD", "HEAD")]
        [InlineData("GET", "PUT", "GET")]
        public void XHttpMethodOverrideHandler_Should_Change_Method_correctly(
            string method, string xHttpMethodValue, string expectedMethod)
        {
            // arrange
            var innerHandlder = new DummyInnerHandler();
            var handler = (HttpMessageHandler) new XHttpMethodOverrideHandler()
                              {
                                  InnerHandler = innerHandlder
                              };
            var request = new HttpRequestMessage(new HttpMethod(method),
                "http://localhost:12345/foo/bar");

            request.Headers.Add(XHttpMethodOverrideHandler.XOVERRIDEHEADER, xHttpMethodValue);
            var invoker = new HttpMessageInvoker(handler);


            // act
            var result = invoker.SendAsync(request, new CancellationToken());

            // assert
            Assert.Equal(expectedMethod, innerHandlder.Request.Method.Method);
        }

        class DummyInnerHandler : HttpMessageHandler
        {
            private HttpRequestMessage _request;

            public HttpRequestMessage Request
            {
                get { return _request; }
            }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, 
                CancellationToken cancellationToken)
            {
                _request = request;
                return null;
            }
        }
    }
}
