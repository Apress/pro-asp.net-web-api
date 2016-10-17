using System.Web.Http;

namespace SimpleQueryStringModelBindingSample.Controllers {
    public class ValuesController : ApiController {
        public int Get(int param1, int param2) {
            return param1 + param2;
        }
    }
}