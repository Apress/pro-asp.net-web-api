using System.Web.Http;
using System.Web.Routing;

namespace RouteDataMappingSample.App_Start {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.MapHttpRoute(
                "DefaultApi",
                routeTemplate: "api/{controller}.{extension}",
                defaults: new { },
                constraints: new { extension = "json|xml" }
                );
        }
    }
}