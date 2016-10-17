using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InvalidModelValidatorProviderSample.Models;
using System.ComponentModel.DataAnnotations;

namespace InvalidModelValidatorProviderSample.APIs {

    [FromUri]
    public class UriParameter {

        [Required]
        public int Id { get; set; }
    }

    public class CarsController : ApiController {

        private readonly CarsContext _carsCtx = new CarsContext();

        // GET /api/cars/{id}
        public Car GetCar(UriParameter uriParameter) {

            var carTuple = _carsCtx.GetSingle(uriParameter.Id);

            if (!carTuple.Item1) {

                var response = Request.CreateResponse(HttpStatusCode.NotFound);
                throw new HttpResponseException(response);
            }

            return carTuple.Item2;
        }

        // POST /api/cars
        public HttpResponseMessage PostCar(Car car) {

            var createdCar = _carsCtx.Add(car);
            var response = Request.CreateResponse(HttpStatusCode.Created, createdCar);
            response.Headers.Location = new Uri(
                Url.Link("DefaultApiRoute", new { id = createdCar.Id }));

            return response;
        }
    }
}