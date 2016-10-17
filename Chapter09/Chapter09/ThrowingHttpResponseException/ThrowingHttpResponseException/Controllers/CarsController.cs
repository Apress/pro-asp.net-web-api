using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ThrowingHttpResponseException.Controllers {

    public class CarsController : ApiController {
        
        public string[] GetCars() {

            //stop processing and throw the HttpResponseException
            var response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            throw new HttpResponseException(response);
        }
    }
}