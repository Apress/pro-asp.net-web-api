using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiDocumentation.ApiExplorerExtensions;
using WebApiDocumentation.Entities;

namespace WebApiDocumentation.Controllers {
    public class CarsController : ApiController {
        private readonly List<Car> _cars;

        public CarsController() {
            _cars = new List<Car>() {
                    new Car() {
                                    Id = 17,
                                    Make = "VW",
                                    Model = "Golf",
                                    Year = 1999,
                                    Price = 1500f
                                },
                    new Car() {
                                    Id = 24,
                                    Make = "Porsche",
                                    Model = "911",
                                    Year = 2011,
                                    Price = 100000f
                                },
                    new Car() {
                                    Id = 30,
                                    Make = "Mercedes",
                                    Model = "A-Class",
                                    Year = 2007,
                                    Price = 10000f
                                }
                };
        }

        public List<Car> Get() {
            return _cars;
        }

        public Car Post(Car car) {
            car.Id = _cars.Max(c => c.Id) + 1;
            _cars.Add(car);
            return car;
        }


        // comment this to test Listing 18-33 and 18-34.
        [ApiDocumentation("Gets a car by its ID.")]
        [ApiParameterDocumentation("id", "The ID of the car.")]
        public Car Get(int id) {
            return _cars.FirstOrDefault(c => c.Id == id);
        }


        // uncomment this to test Listing 18-33 and 18-34.
        //public HttpResponseMessage Get(int id) {
        //    return Request.CreateResponse(HttpStatusCode.OK,
        //                                    _cars.FirstOrDefault(c => c.Id == id));
        //}


        public Car Put([FromUri] int id, Car car) {
            _cars[_cars.FindIndex(c => c.Id == id)] = car;
            return car;
        }

        public HttpResponseMessage Delete(int id) {
            _cars.RemoveAt(_cars.FindIndex(c => c.Id == id));
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}