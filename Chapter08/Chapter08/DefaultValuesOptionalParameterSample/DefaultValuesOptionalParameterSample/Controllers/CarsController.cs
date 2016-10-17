using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace DefaultValuesOptionalParameterSample.Controllers {

    public class CarsController : ApiController {

        public string[] Get() {

            //this will hold the value of the id route parameter
            //if it is not provided, then it will be null
            var idRouteParamValue = ControllerContext.RouteData.Values["id"];

            return new string[] { 
                "Cars 1",
                "Cars 2",
                "Cars 3"
            };
        }
    }
}