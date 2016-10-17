using System.Web.Http;

namespace HttpClientConneg.WebApi.Controllers {
    public class CustomerController : ApiController {
         public Customer Get(int id) {
             return new Customer() {
                Id = id,
                Name = "Apress"
             };
         }
    }

    public class Customer {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}