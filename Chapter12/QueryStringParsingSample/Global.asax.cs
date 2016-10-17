using System;
using System.Web.Routing;
using QueryStringParsingSample.App_Start;

namespace QueryStringParsingSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}