using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace MultipleRoutes.Controllers {

    public class VehiclesController : ApiController {

        public string[] Get(string vehicletype) {

            return new string[] { 
                string.Format("Vehicle 1 ({0})", vehicletype),
                string.Format("Vehicle 2 ({0})", vehicletype),
                string.Format("Vehicle 3 ({0})", vehicletype),
            };
        }
    }
}