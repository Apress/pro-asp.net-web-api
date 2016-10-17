using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ExceptionFilterOrdering.Filters;
using System.Web.Http;

namespace ExceptionFilterOrdering.APIs {

    [ExceptionLogger]
    public class CarsController : ApiController {

        [SecondaryExceptionLoggerAttribute]
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