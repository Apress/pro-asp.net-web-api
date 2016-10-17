using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace RoutingIntro.Controllers {

    public class Car {

        public string Name { get; set; }
    }

    public class CarsController : ApiController {

        //GET api/cars
        //GET api/cars?foo=bar
        //GET api/cars?bar=foo
        public string[] Get() {

            return new string[] { 
                "Car 1",
                "Car 2",
                "Car 3",
                "Car 4"
            };
        }

        //GET api/cars/1
        public string Get(int id) {

            return string.Format("Car {0}", id);
        }

        //GET api/cars?type=SUV
        public string[] GetCarsByType(string type) {

            return new string[] { 
                "Car 2",
                "Car 4"
            };
        }

        //GET api/cars?make=make1
        public string[] GetCarsByMake(string make) {

            return new string[] { 
                "Car 1",
                "Car 3",
                "Car 4"
            };
        }

        //GET api/cars?make=make1&type=SUV
        public string[] GetCarsByMakeByType(string make, string type) {

            return new string[] { 
                "Car 4"
            };
        }
    }
}