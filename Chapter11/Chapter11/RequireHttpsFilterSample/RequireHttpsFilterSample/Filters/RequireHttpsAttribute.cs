using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;

namespace RequireHttpsFilterSample.Filters {

    public class RequireHttpsAttribute : AuthorizationFilterAttribute {

        public override void OnAuthorization(HttpActionContext actionContext) {

            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps) {

                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden) {
                    Content = new StringContent("SSL required")
                };
            }
        }
    }
}