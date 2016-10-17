using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;

namespace SelfHostingSamples
{
    public class TestController : ApiController
    {
        public string Get()
        {
            return DateTime.Now.ToString();
        }
    }

}
