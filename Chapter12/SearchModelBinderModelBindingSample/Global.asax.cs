using System;
using System.Web.Routing;
using SearchModelBinderModelBindingSample.App_Start;

namespace SearchModelBinderModelBindingSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}