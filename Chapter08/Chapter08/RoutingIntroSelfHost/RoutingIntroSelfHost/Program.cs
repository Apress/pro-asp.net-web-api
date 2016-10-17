using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace RoutingIntroSelfHost {

    class Program {

        static void Main(string[] args) {

            var config = new HttpSelfHostConfiguration(new Uri("http://localhost:5478"));

            config.Routes.MapHttpRoute(
                "DefaultHttpRoute", 
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            using (HttpSelfHostServer server = new HttpSelfHostServer(config)) {

                server.OpenAsync().Wait();

                Console.WriteLine("Press Enter to quit.");
                Console.ReadLine();
            }
        }
    }
}
