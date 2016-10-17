using System;
using System.Web.Routing;
using XHeaderValueProviderModelBindingSample.App_Start;

namespace XHeaderValueProviderModelBindingSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}