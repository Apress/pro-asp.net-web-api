using System.Web.Http;
using System.Web.Routing;

namespace CamelCaseJsonFormatterSample.App_Start {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional}
                );
        }
    }
}