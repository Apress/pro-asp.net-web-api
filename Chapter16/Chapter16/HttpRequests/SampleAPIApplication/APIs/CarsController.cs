using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SampleAPIApplication.Models;
using System.Threading;

namespace SampleAPIApplication.APIs {

    public class CarsController : ApiController {

        private readonly CarsContext _carsCtx =
            new CarsContext();

        public IEnumerable<Car> Get() {

            //Sleep 1 second to simulate a long running api call
            Thread.Sleep(1000);

            var cars = _carsCtx.All;
            return cars;
        }

        public HttpResponseMessage PostCar(Car car) {

            var createdCar = _carsCtx.Add(car);
            var response = Request.CreateResponse(HttpStatusCode.Created, createdCar);
            response.Headers.Location = new Uri(
                Url.Link(
                    "DefaultHttpRoute", new {
                        id = createdCar.Id,
                        controller = "cars"
                    }));

            return response;
        }

        public Car PutCar(int id, Car car) {

            car.Id = id;

            if (!_carsCtx.TryUpdate(car)) {

                var response =
                    new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return car;
        }

        public HttpResponseMessage DeleteCar(int id) {

            if (!_carsCtx.TryRemove(id)) {

                var response =
                    new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        //Second GET method
        public Car GetCar(int id) {

            var carTuple = _carsCtx.GetSingle(id);

            if (!carTuple.Item1) {

                var response =
                    new HttpResponseMessage(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return carTuple.Item2;
        }
    }
}