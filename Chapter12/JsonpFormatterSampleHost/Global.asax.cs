using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using JsonpFormatterSampleHost.App_Start;
using JsonpFormatterSampleHost.Formatters;

namespace JsonpFormatterSampleHost {
    public class Global : HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            var config = GlobalConfiguration.Configuration;

            config.Formatters.Remove(config.Formatters.JsonFormatter);
            config.Formatters.Insert(0, new JsonpMediaTypeFormatter());

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}