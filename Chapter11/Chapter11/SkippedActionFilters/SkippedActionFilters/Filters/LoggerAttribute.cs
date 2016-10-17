using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Diagnostics;
using System.Web.Http.Controllers;
using System.Net;
using System.Net.Http;

namespace SkippedActionFilters.Filters {

    public class LoggerAttribute : ActionFilterAttribute {

        private const string _loggerName = "Logger";

        public override void OnActionExecuting(
            HttpActionContext actionContext) {

            //terminate the request by set a new response 
            //to actionContext.Response
            actionContext.Response = new HttpResponseMessage(
                HttpStatusCode.NotFound
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