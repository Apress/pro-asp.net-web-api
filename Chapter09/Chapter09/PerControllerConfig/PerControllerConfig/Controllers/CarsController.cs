using PerControllerConfig.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PerControllerConfig.Controllers {

    [OnlyJsonConfig]
    public class CarsController : ApiController {

        public string[] GetCars() {

            var foo = this.ControllerContext;

            return new[] { 
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}