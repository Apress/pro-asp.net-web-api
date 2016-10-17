using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;
using System.Web.Security;
using System.Web.SessionState;

namespace InvalidModelValidatorProviderSample {

    public class Global : HttpApplication {

        protected void Application_Start(object sender, EventArgs e) {

            var config = GlobalConfiguration.Configuration;
            config.Routes.MapHttpRoute(
                "DefaultApiRoute",
                "api/{controller}/{id}",
                new { id = RouteParameter.Optional }
            );

            ////NOTE: Just remove the InvalidModelValidatorProvider
            //config.Services.RemoveAll(
            //    typeof(ModelValidatorProvider), 
            //    validator => validator is InvalidModelValidatorProvider);

            //NOTE: Remove all validator providers except for DataAnnotationsModelValidatorProvider
            config.Services.RemoveAll(
                typeof(ModelValidatorProvider),
                validator => !(validator is DataAnnotationsModelValidatorProvider));
        }
    }
}