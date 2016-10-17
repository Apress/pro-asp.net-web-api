using System.Web.Http;
using System.Web.Routing;

namespace JsonpFormatterSampleHost.App_Start {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}/{format}",
                defaults: new {id = RouteParameter.Optional, format = RouteParameter.Optional}
                );
        }
    }
}