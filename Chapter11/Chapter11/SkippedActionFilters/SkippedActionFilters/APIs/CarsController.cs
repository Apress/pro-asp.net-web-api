using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using SkippedActionFilters.Filters;

namespace SkippedActionFilters.APIs {

    [Logger]
    public class CarsController : ApiController {

        [SecondaryLogger]
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