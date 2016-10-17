using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using ReturningHttpResponseMessageWithObjSample.Models;

namespace ReturningHttpResponseMessageWithObjSample.Controllers {

    public class CarsController : ApiController {

        public HttpResponseMessage GetCars() {

            var cars = new string[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };

            HttpResponseMessage response = 
                Request.CreateResponse<string[]>(HttpStatusCode.OK, cars);
            response.Headers.Add("X-Foo", "Bar");
            return response;
        }
    }
}