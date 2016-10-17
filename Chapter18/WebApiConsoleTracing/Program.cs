using System;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Web.Http.Tracing;
using WebApiConsoleTracing.App_Start;
using WebApiConsoleTracing.Tracers;

namespace WebApiConsoleTracing {
internal class Program {
    private static void Main(string[] args) {
        var config = new HttpSelfHostConfiguration("http://localhost:8080");

        config.Routes.MapHttpRoute(
            name: "DefaultApi",
            routeTemplate: "api/{controller}/{id}",
            defaults: new {id = RouteParameter.Optional}
            );

        var traceWriter = new ConsoleTraceWriter();
        config.Services.Replace(typeof (ITraceWriter), traceWriter);

        using (var server = new HttpSelfHostServer(config)) {
            server.OpenAsync().Wait();
            Console.WriteLine("Press Enter to quit.");
            Console.ReadLine();
        }
    }
}
}