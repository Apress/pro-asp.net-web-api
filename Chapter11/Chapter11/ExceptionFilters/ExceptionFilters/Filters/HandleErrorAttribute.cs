using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Diagnostics;

namespace ExceptionFilters.Filters {

    public class HandleErrorAttribute : ExceptionFilterAttribute {

        public override void OnException(
            HttpActionExecutedContext actionExecutedContext) {

            var actionCtx = actionExecutedContext.ActionContext;
            var controllerCtx = actionCtx.ControllerContext;

            Trace.TraceInformation(
                "Exception occured. Controller: {0}, action: {1}. Exception message: {2}",
                controllerCtx.ControllerDescriptor.ControllerName,
                actionCtx.ActionDescriptor.ActionName,
                actionExecutedContext.Exception.Message);
        }
    }
}