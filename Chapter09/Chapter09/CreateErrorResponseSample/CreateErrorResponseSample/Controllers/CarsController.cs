using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Http;
using System.Net.Http;

namespace CreateErrorResponseSample.Controllers {

    public class CarsController : ApiController {

        public string[] GetCars() {

            try {

                int left = 10,
                    right = 0;

                var result = left / right;

            }
            catch (DivideByZeroException ex) {

                var faultedResponse = Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError, ex);

                throw new HttpResponseException(faultedResponse);
            }

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }

        public HttpResponseMessage GetCar(int id) {

            if ((id % 2) != 0) {

                var httpError = new HttpError();
                httpError.Add("id", 
                    "Only \"even numbers\" are accepted as id.");

                return Request.CreateErrorResponse(
                    HttpStatusCode.InternalServerError,
                    httpError);
            }

            return Request.CreateResponse(
                HttpStatusCode.OK,
                string.Format("Car {0}", id));
        }

        //// Directly sending as HttpResponseMessage
        //public HttpResponseMessage GetCars() {

        //    try {

        //        int left = 10,
        //            right = 0;

        //        var result = left / right;

        //    }
        //    catch (DivideByZeroException ex) {

        //        return Request.CreateErrorResponse(
        //            HttpStatusCode.InternalServerError, ex);
        //    }

        //    var cars = new[] { 
        //        "Car 1",
        //        "Car 2",
        //        "Car 3"
        //    };

        //    return Request.CreateResponse(HttpStatusCode.OK, cars);
        //}
    }
}