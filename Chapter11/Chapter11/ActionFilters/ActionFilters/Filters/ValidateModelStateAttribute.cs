using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;

namespace ActionFilters.Filters {

    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Method, 
        AllowMultiple = false, Inherited = true)]
    public class ValidateModelStateAttribute : ActionFilterAttribute {

        public override void OnActionExecuting(
            HttpActionContext actionContext) {

            if (!actionContext.ModelState.IsValid) {

                actionContext.Response = 
                    actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest, 
                        actionContext.ModelState);
            }
        }
    }
}