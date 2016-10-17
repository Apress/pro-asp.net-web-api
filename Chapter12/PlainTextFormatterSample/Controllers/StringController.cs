using System.Web.Http;

namespace PlainTextFormatterSample.Controllers {
    public class StringController : ApiController {
        public string Get() {
            return "Hello World";
        }
    }
}