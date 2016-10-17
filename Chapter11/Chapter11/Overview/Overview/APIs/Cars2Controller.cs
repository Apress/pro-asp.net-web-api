using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Overview.Models;
using Overview.Filters;
using System.Diagnostics;

namespace Overview.APIs {

    [Logger]
    public class Cars2Controller : ApiController {

        private readonly CarsContext _carsContext = new CarsContext();

        public IEnumerable<Car> GetCars() {

            return _carsContext.All;
        }

        public Car GetCar(int id) {

            return _carsContext.GetSingle(car => car.Id == id);
        }
    }
}