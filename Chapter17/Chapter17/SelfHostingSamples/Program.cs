using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace SelfHostingSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            // classic
            var config = new HttpSelfHostConfiguration("http://localhost:18081");

            // HTTPS
            //var config = new HttpsSelfHostConfiguration("http://localhost:18081");

            // NTLM
            //var config = new NtlmHttpSelfHostConfiguration("http://localhost:18081");


            // BASIC AUTHENTICATION
            //var config = new BasicAuthenticationSelfHostConfiguration("http://localhost:18081",
            //    (un, pwd) => un == "johndoe" && pwd == "123456");



            config.Routes.MapHttpRoute(
                "API Default", "api/{controller}/{id}",
                new { id = RouteParameter.Optional });

            using (var server = new HttpSelfHostServer(config))
            {
                server.OpenAsync().Wait();
                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }

    }
}
