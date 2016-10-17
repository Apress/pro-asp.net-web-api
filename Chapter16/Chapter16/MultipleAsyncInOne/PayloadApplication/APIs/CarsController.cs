using System.Collections.Generic;
using System.Threading;
using System.Web.Http;
using PayloadApplication.Models;

namespace PayloadApplication.APIs {

    public class CarsController : ApiController {

        readonly CarsContext _carsContext = new CarsContext();

        [HttpGet]
        public IEnumerable<Car> Cheap() {

            Thread.Sleep(1000);

            return _carsContext.GetCars(car => car.Price < 50000);
        }

        [HttpGet]
        public IEnumerable<Car> Expensive() {

            Thread.Sleep(1000);

            return _carsContext.GetCars(car => car.Price >= 50000);
        }
    }
}