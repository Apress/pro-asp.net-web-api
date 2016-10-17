using System;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Routing;
using RequestHeaderMappingSample.App_Start;

namespace RequestHeaderMappingSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            var config = GlobalConfiguration.Configuration;
            config.Formatters.RemoveAt(0);
            var jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.MediaTypeMappings.Add(
                new RequestHeaderMapping(
                    "Referer", "http://localhost:1501/", 
                    StringComparison.InvariantCultureIgnoreCase, 
                    false, 
                    "text/xml"));
            config.Formatters.Insert(0, jsonFormatter);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}