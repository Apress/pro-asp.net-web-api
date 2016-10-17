using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PerControllerConfig.Controllers {
    
    public class VehiclesController : ApiController {

        public string[] GetVehicles() {

            return new[] { 
                "Vehicle 1",
                "Vehicle 2",
                "Vehicle 3"
            };
        }
    }
}