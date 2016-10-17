using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace WebApiNLogTracing.Controllers {
    public class ValuesController : ApiController {
        // GET api/values
        public IEnumerable<string> Get() {
            var traceWriter = Configuration.Services.GetTraceWriter();
            if (null != traceWriter) {
                traceWriter.Trace(Request, "WebApiNLogTracing.Controllers",
                                  TraceLevel.Info,
                                  (traceRecord) => { traceRecord.Message = "Read all values."; });
            }
            return new string[] {"value1", "value2"};
        }

        // GET api/values/5
        public string Get(int id) {
            var traceWriter = Configuration.Services.GetTraceWriter();
            if (traceWriter != null) {
                traceWriter.Debug(Request, "WebApiNLogTracing.Controllers",
                                  string.Format("Read value {0}", id));
            }
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value) {}

        // PUT api/values/5
        public void Put(int id, [FromBody] string value) {}

        // DELETE api/values/5
        public void Delete(int id) {}
    }
}