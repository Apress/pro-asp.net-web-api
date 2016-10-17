using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Security;
using System.Web.SessionState;

namespace GreaterThanAttributeSample {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "DefaultHttpRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            config.Services.RemoveAll(
                typeof(ModelValidatorProvider),
                validator => !(validator is DataAnnotationsModelValidatorProvider));
        }
    }
}