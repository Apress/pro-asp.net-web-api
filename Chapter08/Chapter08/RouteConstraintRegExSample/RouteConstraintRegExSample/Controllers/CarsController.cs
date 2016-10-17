using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RouteConstraintRegExSample.APIs {

    public class CarsController : ApiController {

        public string[] Get() {

            return new string[] { 
                "Cars 1",
                "Cars 2",
                "Cars 3"
            };
        }

        public string Get(int id) {

            return string.Format("Car {0}", id);
        }
    }
}