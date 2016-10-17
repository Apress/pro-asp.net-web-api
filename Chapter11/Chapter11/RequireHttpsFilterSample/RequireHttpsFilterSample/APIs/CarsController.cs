using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using RequireHttpsFilterSample.Filters;

namespace RequireHttpsFilterSample.APIs {

    [RequireHttps]
    public class CarsController : ApiController {

        public string[] GetCars() {

            return new string[] { 
                "Car 1",
                "Car 2",
                "Car 3",
                "Car 4"
            };
        }
    }
}