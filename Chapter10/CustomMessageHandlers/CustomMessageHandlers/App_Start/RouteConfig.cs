using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using CustomMessageHandlers.MessageHandlers;

namespace CustomMessageHandlers.App_Start {
    public class RouteConfig {
        public static void RegisterRoutes(RouteCollection routes) {
            routes.MapHttpRoute(
                name: "Secret Api",
                routeTemplate: "secretapi/{controller}/{id}",
                defaults: new {id = RouteParameter.Optional},
                constraints: null,
                handler: new ApiKeyProtectionMessageHandler() {
                                                                  InnerHandler =
                                                                      new HttpControllerDispatcher(
                                                                      GlobalConfiguration.Configuration)
                                                              });

            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new {controller = "Values", id = RouteParameter.Optional}
                );
        }
    }
}