using HTTPCaching.API.MessageHandlers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace HTTPCaching.API {

    public class Global : System.Web.HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            var eTagHandler = new HttpCachingHandler("Accept", "Accept-Charset");
            eTagHandler.CacheInvalidationStore.Add(requestUri => {

                if (requestUri.StartsWith(
                    "/api/cars/",
                    StringComparison.InvariantCultureIgnoreCase)) {

                    return new[] { "/api/cars" };
                }

                return new string[0];
            });

            config.MessageHandlers.Insert(0, eTagHandler);
        }
    }
}