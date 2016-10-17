using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace HttpRequests {
    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            // increases the connectionManagement/maxconnection limit
            // if you are on .NET 4.0. We don't need to adjust this setting here
            // as we are under .NET 4.5. In .NET 4.5, this value is set to Int32.MaxValue by default.
            // System.Net.ServicePointManager.DefaultConnectionLimit = int.MaxValue;

            GlobalConfiguration.Configuration.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );
        }
    }
}