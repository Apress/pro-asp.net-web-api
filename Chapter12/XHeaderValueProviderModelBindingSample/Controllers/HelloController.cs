using System.Web.Http;
using System.Web.Http.ValueProviders;
using XHeaderValueProviderModelBindingSample.ValueProviders;

namespace XHeaderValueProviderModelBindingSample.Controllers {
    public class HelloController : ApiController {
        public string Get([ValueProvider(typeof (XHeaderValueProviderFactory))] string name) {
            return "Hello, " + name;
        }
    }
}

