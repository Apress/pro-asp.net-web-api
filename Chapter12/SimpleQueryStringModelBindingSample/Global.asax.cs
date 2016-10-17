using System;
using System.Web.Routing;
using SimpleQueryStringModelBindingSample.App_Start;

namespace SimpleQueryStringModelBindingSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}