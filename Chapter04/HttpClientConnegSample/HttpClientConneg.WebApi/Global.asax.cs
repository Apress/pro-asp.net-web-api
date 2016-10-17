using System;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using HttpClientConneg.WebApi.App_Start;

namespace HttpClientConneg.WebApi {
    public class Global : HttpApplication {
        protected void Application_Start(object sender, EventArgs e) {
            var config = GlobalConfiguration.Configuration;
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}