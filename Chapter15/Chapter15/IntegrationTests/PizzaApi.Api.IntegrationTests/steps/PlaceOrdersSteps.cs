using System;
using System.Linq;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using PizzaApi.Api.App_Start;
using PizzaApi.Domain;
using TechTalk.SpecFlow;
using Xunit;

namespace PizzaApi.Api.IntegrationTests.steps
{
    [Binding]
    public class PlaceOrdersSteps
    {
        
        [BeforeScenario]
        public void CreateVirtualServer()
        {
             var server = new VirtualServer(WebApiConfig.Register,
                Convert.ToBoolean(ConfigurationManager.AppSettings["UseSelfHosting"]));
             ScenarioContext.Current[TestContextKeys.VirtualServer] = server;
        }

        [AfterScenario]
        public void DisposeVirtualServer()
        {
            var server = ScenarioContext.Current.Get<VirtualServer>(TestContextKeys.VirtualServer);
            if (server != null)
            {
                server.Dispose();
            }

        }

        [Given(@"I have an order for a mixture of pizzas")]
        public void GivenIHaveAnOrderForAMixtureOfPizzas()
        {
            var order = new Order();
            order.Items = new[]
                              {
                                  new OrderItem()
                                      {
                                          Name = "Hawaiian",
                                          Quantity = 2
                                      }, 
                                      new OrderItem()
                                          {
                                              Name = "Meat Feast",
                                              Quantity = 1
                                          } 
                              };
            ScenarioContext.Current[TestContextKeys.NewOrder] = order;
        }
        
        [Given(@"it is for a particular customer")]
        public void GivenItIsForAParticularCustomer()
        {
            const string CustomerName = "SomeCustomer";
            var order = ScenarioContext.Current.Get<Order>(TestContextKeys.NewOrder);
            ScenarioContext.Current[TestContextKeys.CustomerName] = CustomerName;
            order.CustomerName = CustomerName;
        }
        
        [When(@"I place the order")]
        public void WhenIPlaceTheOrder()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, ConfigurationManager.AppSettings["BaseUrl"]
                + "api/Order");
            var server  = ScenarioContext.Current.Get<VirtualServer>(TestContextKeys.VirtualServer);
            var newOrder = ScenarioContext.Current.Get<Order>(TestContextKeys.NewOrder);
            request.Content = new ObjectContent<Order>(newOrder, new JsonMediaTypeFormatter());
            ScenarioContext.Current[TestContextKeys.Request] = request;
            var response = server.Send(request);
            ScenarioContext.Current[TestContextKeys.Response] = response;
            ScenarioContext.Current[TestContextKeys.OrderUrl] = response.Headers.Location;

        }

        [When(@"retrive the order")]
        public void WhenRetriveTheOrder()
        {
            var server = ScenarioContext.Current.Get<VirtualServer>(TestContextKeys.VirtualServer);
            var request = new HttpRequestMessage(HttpMethod.Get,
               ScenarioContext.Current.Get<Uri>(TestContextKeys.OrderUrl));
            var response = server.Send(request);
            var retrievedOrder = response.Content.ReadAsAsync<Order>().Result;
            ScenarioContext.Current[TestContextKeys.RetrievedOrder] = retrievedOrder;            
        }

        [Then(@"system must have priced the order")]
        public void ThenSystemMustHavePricedTheOrder()
        {
            var retrievedOrder = ScenarioContext.Current.Get<Order>(TestContextKeys.RetrievedOrder);
            Assert.True(retrievedOrder.TotalPrice > 0);
        }

        [Then(@"system must have saved the order")]
        public void ThenSystemMustHaveSavedTheOrder()
        {
            var retrievedOrder = ScenarioContext.Current.Get<Order>(TestContextKeys.RetrievedOrder);
            Assert.True(retrievedOrder.Id != 0);            
        }

        [Then(@"saved order must contain same pizzas")]
        public void ThenSavedOrderMustContainSamePizzas()
        {
            var retrievedOrder = ScenarioContext.Current.Get<Order>(TestContextKeys.RetrievedOrder);
            var newOrder = ScenarioContext.Current.Get<Order>(TestContextKeys.NewOrder);
            Assert.Equal(newOrder.Items.Count(), retrievedOrder.Items.Count());
            Assert.Equal(newOrder.Items.First().Name, retrievedOrder.Items.First().Name);
            Assert.Equal(newOrder.Items.First().Quantity, retrievedOrder.Items.First().Quantity);
        }
        
        [Then(@"saved order must have the name of the customer")]
        public void ThenSavedOrderMustHaveTheNameOfTheCustomer()
        {
            var retrievedOrder = ScenarioContext.Current.Get<Order>(TestContextKeys.RetrievedOrder);
            var customerName = ScenarioContext.Current.Get<string>(TestContextKeys.CustomerName);
            Assert.Equal(customerName, retrievedOrder.CustomerName);            
        }
    }
}
