using System.Web.Http;
using System.Web.Routing;
using CustomMessageHandlers.App_Start;
using CustomMessageHandlers.MessageHandlers;

namespace CustomMessageHandlers {
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class WebApiApplication : System.Web.HttpApplication {
        protected void Application_Start() {
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var config = GlobalConfiguration.Configuration;
            config.MessageHandlers.Add(new XHttpMethodOverrideHandler());
            config.MessageHandlers.Add(new XPoweredByHeaderHandler());
        }
    }
}