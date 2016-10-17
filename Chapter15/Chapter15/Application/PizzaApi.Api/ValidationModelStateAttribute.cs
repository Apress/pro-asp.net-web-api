using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace PizzaApi.Api
{
    [AttributeUsage(
     AttributeTargets.Class | AttributeTargets.Method,
     AllowMultiple = false, Inherited = true)]
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(
            HttpActionContext actionContext)
        {

            if (!actionContext.ModelState.IsValid)
            {

                actionContext.Response =
                    actionContext.Request.CreateErrorResponse(
                        HttpStatusCode.BadRequest,
                        actionContext.ModelState);
            }
        }


    }

}