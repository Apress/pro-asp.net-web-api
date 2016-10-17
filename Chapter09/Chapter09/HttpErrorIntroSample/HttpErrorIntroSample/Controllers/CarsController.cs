using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Net.Http;
using System.Net;

namespace HttpErrorIntroSample.Controllers {

    public class CarsController : ApiController {

        public string[] GetCars() {
            
            try {

                int left = 10,
                    right = 0;

                var result = left / right;

            }
            catch (DivideByZeroException ex) {

                var faultedResponse = Request.CreateResponse(
                    HttpStatusCode.InternalServerError, 
                    new HttpError(ex, includeErrorDetail: true));

                throw new HttpResponseException(faultedResponse);
            }

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}