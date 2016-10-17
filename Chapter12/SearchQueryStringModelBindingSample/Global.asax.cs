using System;
using System.Web.Routing;
using SearchQueryStringModelBindingSample.App_Start;

namespace SearchQueryStringModelBindingSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}