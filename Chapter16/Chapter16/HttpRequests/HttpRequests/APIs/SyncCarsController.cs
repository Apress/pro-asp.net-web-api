using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HttpRequests.Models;
using System.Net;
using Newtonsoft.Json;

namespace HttpRequests.APIs {

    public class SyncCarsController : ApiController {

        //HTTP service base address
        const string CountryAPIBaseAddress = "http://localhost:11338/api/cars";

        public IEnumerable<Car> Get() {

            using (WebClient client = new WebClient()) {
                
                var content = client.DownloadString(CountryAPIBaseAddress);
                var cars = JsonConvert.DeserializeObject<List<Car>>(content);
                return cars.Where(x => x.Price > 30000.00F);
            }
        }
    }
}