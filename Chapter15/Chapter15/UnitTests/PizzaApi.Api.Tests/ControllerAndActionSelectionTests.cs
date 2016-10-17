using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Hosting;
using PizzaApi.Api.App_Start;
using PizzaApi.Api.Controllers;
using Xunit;
using Xunit.Extensions;

namespace PizzaApi.Api.Tests
{
    public class ControllerAndActionSelectionTests
    {
[Theory]
[InlineData("http://localhost:12345/api/order/123", "GET", typeof(OrderController), "Get")]
[InlineData("http://localhost:12345/api/order", "POST", typeof(OrderController), "Post")]
[InlineData("http://localhost:12345/api/order/123", "PUT", typeof(OrderController), "Put")]
[InlineData("http://localhost:12345/api/order", "GET", typeof(OrderController), "Get")]
[InlineData("http://localhost:12345/api/order/123/OrderItem", "GET", typeof(OrderItemController), "GetItems")]
public void Ensure_Correct_Controller_and_Action_Selected(
    string url,
    string method,
    Type controllerType,
    string actionName)
{
    // arrange
    var config = new HttpConfiguration();
    WebApiConfig.Register(config);
    var actionSelector = config.Services.GetActionSelector();
    var controllerSelector = config.Services.GetHttpControllerSelector();
    var request = new HttpRequestMessage(new HttpMethod(method), url);
    var routeData = config.Routes.GetRouteData(request);
    request.Properties[HttpPropertyKeys.HttpRouteDataKey] = routeData;
    request.Properties[HttpPropertyKeys.HttpConfigurationKey] = config;
            

    // act
    var controllerDescriptor = controllerSelector.SelectController(request);
    var context = new HttpControllerContext(config, routeData, request)
                        {
                            ControllerDescriptor = controllerDescriptor
                        };
    var actionDescriptor = actionSelector.SelectAction(context);

    // assert
    Assert.Equal(controllerType, controllerDescriptor.ControllerType);
    Assert.Equal(actionName, actionDescriptor.ActionName);
}
    }
}
