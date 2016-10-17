using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using PlainTextFormatterSample.App_Start;
using PlainTextFormatterSample.Formatters;

namespace PlainTextFormatterSample {
    public class Global : HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            var config = GlobalConfiguration.Configuration;

            config.Formatters.Add(new PlainTextFormatter());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}