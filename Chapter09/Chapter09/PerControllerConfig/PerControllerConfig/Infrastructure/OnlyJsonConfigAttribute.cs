using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;

namespace PerControllerConfig.Infrastructure {

    public class OnlyJsonConfigAttribute : Attribute, IControllerConfiguration {

        public void Initialize(
            HttpControllerSettings controllerSettings, 
            HttpControllerDescriptor controllerDescriptor) {
            
            var jqueryFormatter = controllerSettings.Formatters
                .FirstOrDefault(x => x.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter));
            controllerSettings.Formatters
                .Remove(controllerSettings.Formatters.XmlFormatter);
            controllerSettings.Formatters
                .Remove(controllerSettings.Formatters.FormUrlEncodedFormatter);
            controllerSettings.Formatters.Remove(jqueryFormatter);

            controllerSettings.Services.Replace(
                typeof(IContentNegotiator),
                new DefaultContentNegotiator(excludeMatchOnTypeOnly: true)
            );
        }
    }
}