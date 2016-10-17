using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.WebHost;
using PizzaApi.Api.App_Start;
using PizzaApi.Domain;
using TechTalk.SpecFlow;
using Xunit;

namespace PizzaApi.Api.IntegrationTests.steps
{
    [Binding]
    public class FormattingSteps
    {

        private const string Url = "http://localhost:12345/api/Order";
        private string _format = null;
        private HttpResponseMessage _response;

        [Given(@"I provide format (.*)")]
        public void GivenIProvideFormat(string format)
        {
            _format = format;
        }

        [When(@"When I request for all orders")]
        public void WhenIRequestOrderData()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url);
            var server = new VirtualServer(WebApiConfig.Register,
                Convert.ToBoolean(ConfigurationManager.AppSettings["UseSelfHosting"]));
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add( new MediaTypeWithQualityHeaderValue(
                _format == "JSON" ? "application/json" : "application/xml"));
            _response = server.Send(request);
        }

        [Then(@"I get back (.+) content type")]
        public void ThenIGetBackContentType(string contentType)
        {
            Assert.Equal(contentType, _response.Content.Headers.ContentType.MediaType);
        }

        [Then(@"content is a set of orders")]
        public void ThenContentIsASetOfOrders()
        {
            var content = _response.Content.ReadAsAsync<IEnumerable<Order>>().Result;
        }

        [When(@"When an error is returned")]
        public void WhenWhenAnErrorIsReturned()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Url + "NotExistent");
            var server = new VirtualServer(WebApiConfig.Register,
                Convert.ToBoolean(ConfigurationManager.AppSettings["UseSelfHosting"]));
            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(
                _format == "JSON" ? "application/json" : "application/xml"));
            _response = server.Send(request);
        }

        [Then(@"message content contains error information")]
        public void ThenMessageContentContainsErrorInformation()
        {
            var error = _response.Content.ReadAsStringAsync().Result;
            Assert.Contains("No Http resource", error, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
