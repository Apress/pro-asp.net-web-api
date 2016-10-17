using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Overview.Models;
using System.Diagnostics;

namespace Overview.APIs {

    public class Cars1Controller : ApiController {

        private readonly CarsContext _carsContext = new CarsContext();

        public IEnumerable<Car> GetCars() {

            log("GetCars");

            return _carsContext.All;
        }

        public Car GetCar(int id) {

            log("GetCar");

            return _carsContext.GetSingle(car => car.Id == id);
        }

        private void log(string actionName) {
            
            Trace.TraceInformation(
                string.Format(
                    "Controller {0}, Action {1} is running...", "Cars", actionName
                )
            );
        }
    }
}