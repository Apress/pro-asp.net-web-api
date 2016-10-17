using System;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Routing;
using RouteDataMappingSample.App_Start;
using RouteDataMappingSample.MediaTypeMappings;

namespace RouteDataMappingSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            var config = GlobalConfiguration.Configuration;

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.
                MediaTypeMappings.Add(new RouteDataMapping("extension", "json", new MediaTypeHeaderValue("application/json")));

            GlobalConfiguration.Configuration.Formatters.XmlFormatter.
                MediaTypeMappings.Add(new RouteDataMapping("extension", "xml", new MediaTypeHeaderValue("application/xml")));


            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}