using System;
using System.Web.Routing;
using FromUriAttributeModelBindingSample.App_Start;

namespace FromUriAttributeModelBindingSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}