using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace RouteConstraintRegExSample {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var routes = GlobalConfiguration.Configuration.Routes;

            routes.MapHttpRoute(
                "DefaultHttpRoute",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { },
                constraints: new { id = @"\d+" }
            );
        }
    }
}