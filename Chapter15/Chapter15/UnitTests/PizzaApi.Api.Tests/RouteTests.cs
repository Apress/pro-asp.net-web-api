using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Routing;
using PizzaApi.Api.App_Start;
using Xunit;
using Xunit.Extensions;

namespace PizzaApi.Api.Tests
{
    public class RouteTests
    {
        [Theory]
        [InlineData("http://localhost:12345/foo/route", "GET", false, null, null)]
        [InlineData("http://localhost:12345/api/order/", "GET", true, "order", null)]
        [InlineData("http://localhost:12345/api/order/123", "GET", true, "order", "123")]
        [InlineData("http://localhost:12345/api/order/123/OrderItem/456", "GET", true, "OrderItem", "123")]
        public void DefaultRoute_Returns_Correct_RouteData(
            string url, string method, bool shouldfound, string controller, string id)
        {
            // arrange
            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
            var request = new HttpRequestMessage(new HttpMethod(method), url);
            
            // act
            var routeData = config.Routes.GetRouteData(request);

            // assert
            Assert.Equal(shouldfound, routeData!=null);
            if (shouldfound)
            {
                Assert.Equal(controller, routeData.Values["controller"]);
                Assert.Equal(id == null ? (object) RouteParameter.Optional : (object)id, routeData.Values["id"]);                                    
            }
        }

    }

}
