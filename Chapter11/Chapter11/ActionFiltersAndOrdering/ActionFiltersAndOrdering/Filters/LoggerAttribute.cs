using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Diagnostics;
using System.Web.Http.Controllers;

namespace ActionFiltersAndOrdering.Filters {

    public class LoggerAttribute : ActionFilterAttribute {

        private const string _loggerName = "Logger";

        public override void OnActionExecuting(
            HttpActionContext actionContext) {

            var controllerCtx = actionContext.ControllerContext;

            LoggerUtil.WriteLog(
                _loggerName,
                "OnActionExecuting",
                controllerCtx.ControllerDescriptor.ControllerName,
                actionContext.ActionDescriptor.ActionName
            );
        }

        public override void OnActionExecuted(
            HttpActionExecutedContext actionExecutedContext) {

            var actionCtx = actionExecutedContext.ActionContext;
            var controllerCtx = actionCtx.ControllerContext;

            LoggerUtil.WriteLog(
                _loggerName,
                "OnActionExecuted",
                controllerCtx.ControllerDescriptor.ControllerName,
                actionCtx.ActionDescriptor.ActionName
            );
        }
    }
}