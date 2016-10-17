using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;
using Xunit;

namespace PizzaApi.Api.Tests
{
    public class ValidateModelStateAttributeTests
    {
        [Fact]
        public void Should_Return_BadRequest_If_ModelState_Invalid()
        {
            // arrange
            var filter = new ValidateModelStateAttribute();
            var context = new HttpActionContext(
                new HttpControllerContext(new HttpConfiguration(), 
                    new HttpRouteData(new HttpRoute("SomePattern")), 
                    new HttpRequestMessage()), 
                    new ReflectedHttpActionDescriptor());
            context.ModelState.AddModelError("foo", "some error");

            // act
            filter.OnActionExecuting(context);

            // assert
            Assert.NotNull(context.Response);
            Assert.Equal(HttpStatusCode.BadRequest, context.Response.StatusCode);
        }

        
    }
}
