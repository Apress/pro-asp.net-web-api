using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using PizzaApi.Api.App_Start;

namespace PizzaApi.Api.IntegrationTests
{
    public class VirtualServer : IDisposable
    {
        private readonly bool _useSelfHosting;
        private readonly Action<HttpConfiguration> _setupConfiguration;
        private Func<HttpRequestMessage, Task<HttpResponseMessage>> _invoker;
        private HttpSelfHostServer _server;

        public VirtualServer(Action<HttpConfiguration> setupConfiguration, bool useSelfHosting = false)
        {
            _setupConfiguration = setupConfiguration;
            _useSelfHosting = useSelfHosting;
            if (useSelfHosting)
            {
                var config = new HttpSelfHostConfiguration(new Uri(ConfigurationManager.AppSettings["BaseUrl"]));
                _setupConfiguration(config);
                _server = new HttpSelfHostServer(config);
                _server.OpenAsync().Wait();
                var client = new HttpClient();
                _invoker = client.SendAsync;
            }
            else
            {
                var config = new HttpConfiguration();
                _setupConfiguration(config);
                var server = new HttpServer(config);
                _invoker = (req) =>
                               {
                                   return new HttpMessageInvoker(server).SendAsync(req, new CancellationToken());
                               };
            }
        }

        public HttpResponseMessage Send(HttpRequestMessage request)
        {
            return _useSelfHosting ? SendSelfHosted(request) : SendHttpServer(request);
        }

        private HttpResponseMessage SendHttpServer(HttpRequestMessage request)
        {
           
            return _invoker(request)
                .Result;
        }

        private HttpResponseMessage SendSelfHosted(HttpRequestMessage request)
        {
            return _invoker(request)
                 .Result;
        }

        public void Dispose()
        {
            if (_server != null)
            {
                _server.CloseAsync().Wait();
                _server.Dispose();
                _server = null;
            }
        }
    }
}
