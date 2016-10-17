using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QueryStringParsingSample.Controllers {
    public class ValuesController : ApiController {
        public int Get() {
            var param1 = Request.RequestUri.ParseQueryString().Get("param1");
            var param2 = Request.RequestUri.ParseQueryString().Get("param2");

            if (!string.IsNullOrEmpty(param1) && !String.IsNullOrEmpty(param2)) {
                return Convert.ToInt32(param1) + Convert.ToInt32(param2);
            }

            throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}