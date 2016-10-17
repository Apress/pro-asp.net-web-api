using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

namespace ExceptionFilterOrdering.Filters {

    public class SecondaryExceptionLoggerAttribute : ExceptionFilterAttribute {

        public override void OnException(HttpActionExecutedContext actionExecutedContext) {

            ExLoggerUtil.WriteLog(
                "SecondaryExceptionLogger",
                actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName,
                actionExecutedContext.ActionContext.ActionDescriptor.ActionName,
                actionExecutedContext.Exception.Message
            );
        }
    }
}