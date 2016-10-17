using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ActionMethodSelectorSample.Controllers {

    public class CarsController : ApiController {

        public string[] Get() {

            return GetCars();
        }

        [NonAction]
        public string[] GetCars() {

            return new string[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}