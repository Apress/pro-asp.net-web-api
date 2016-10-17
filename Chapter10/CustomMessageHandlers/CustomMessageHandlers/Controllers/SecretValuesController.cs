using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CustomMessageHandlers.Controllers
{
    public class SecretValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "secret value1", "secret value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "secret value";
        }

        // POST api/values
        public void Post(string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string test)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}