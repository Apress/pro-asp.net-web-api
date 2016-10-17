using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ExceptionFilters.Filters;

namespace ExceptionFilters.APIs {

    [HandleError]
    public class CarsController : ApiController {

        public string[] GetCars() {

            throw new Exception();

            return new string[] { 
                "Car 1",
                "Car 2",
                "Car 3",
                "Car 4"
            };
        }
    }
}