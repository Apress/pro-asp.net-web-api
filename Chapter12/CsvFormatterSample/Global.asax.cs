using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using CsvFormatterSample.App_Start;
using CsvFormatterSample.Formatters;

namespace CsvFormatterSample {
    public class Global : HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            var config = GlobalConfiguration.Configuration;
            config.Formatters.Add(new CarCsvMediaTypeFormatter());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}