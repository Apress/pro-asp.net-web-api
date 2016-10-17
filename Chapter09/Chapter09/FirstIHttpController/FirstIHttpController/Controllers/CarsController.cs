using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;

namespace FirstIHttpController.Controllers {

    public class CarsController : IHttpController {

        public Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken) {

            if (controllerContext.Request.Method != HttpMethod.Get) {

                var notAllowedResponse = 
                    new HttpResponseMessage(HttpStatusCode.MethodNotAllowed);

                return Task.FromResult(notAllowedResponse);
            }
            
            var cars = new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };

            var response = 
                controllerContext.Request.CreateResponse(HttpStatusCode.OK, cars);

            return Task.FromResult(response);
        }
    }
}