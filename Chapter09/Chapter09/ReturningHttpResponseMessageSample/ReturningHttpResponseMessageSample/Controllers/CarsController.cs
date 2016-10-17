using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ReturningHttpResponseMessageSample.Controllers {

    public class CarsController : ApiController {

        public HttpResponseMessage DeleteCar(int id) {

            //Check here if the resource exists
            if (id != 1) {

                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }

            //Delete the car object here

            var response = new HttpResponseMessage(HttpStatusCode.OK);
            return response;
        }
    }
}