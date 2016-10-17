using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace MultipleAsyncInOne.APIs {

    public class Car {

        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public float Price { get; set; }
    }

    public class CarsController : ApiController {

        readonly List<string> _payloadSources = new List<string> { 
            "http://localhost:2700/api/cars/cheap",
            "http://localhost:2700/api/cars/expensive"
        };

        readonly HttpClient _httpClient = new HttpClient();

        [HttpGet]
        public async Task<IEnumerable<Car>> AllCars() {

            var carsResult = new List<Car>();

            foreach (var uri in _payloadSources) {

                var cars = await getCars(uri);
                carsResult.AddRange(cars);
            }

            return carsResult;
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> AllCarsInParallel() {

            var allTasks = _payloadSources.Select(uri => 
                getCars(uri)
            );

            IEnumerable<Car>[] allResults = await Task.WhenAll(allTasks);

            return allResults.SelectMany(cars => cars);
        }

        //private helper which gets the payload and hands it back as IEnumerable<Car>
        private async Task<IEnumerable<Car>> getCars(string uri) {

            var response = await _httpClient.GetAsync(uri);
            var content = await response.Content.ReadAsAsync<IEnumerable<Car>>();

            return content;
        }
    }
}