using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http;

namespace FirstApiControllerSample.Controllers {

    public class CarsController : ApiController {

        public string[] Get() { 

            return new string[] {
                "Car 1",
                "Car 2",
                "Car 3"
            };
        }
    }
}