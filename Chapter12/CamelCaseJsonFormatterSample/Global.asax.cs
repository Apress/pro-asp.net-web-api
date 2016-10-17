using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using CamelCaseJsonFormatterSample.App_Start;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace CamelCaseJsonFormatterSample {
    public class Global : System.Web.HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            var config = GlobalConfiguration.Configuration;
            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings() {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}