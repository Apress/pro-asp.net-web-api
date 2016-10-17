using System.Web.Http;
using FromUriAttributeModelBindingSample.Entities;

namespace FromUriAttributeModelBindingSample.Controllers {
    public class CarController : ApiController {
        public Car Get(int id) {
            return new Car() {
                Id = id,
                Make = "Porsche",
                Model = "911",
                Price = 100000f,
                Year = 2012
            };
        }

        public Car Put([FromUri]int id, [FromBody]Car car) {
            return car;
        }
    }
}