using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace MultipleRoutes {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var routes = GlobalConfiguration.Configuration.Routes;

            routes.MapHttpRoute(
                "VehicleHttpRoute",
                "api/{vehicletype}/{controller}",
                defaults: new { },
                constraints: new { controller = "^vehicles$" }
            );

            routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }
}