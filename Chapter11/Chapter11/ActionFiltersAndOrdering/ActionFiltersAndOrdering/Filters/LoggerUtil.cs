using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;

namespace ActionFiltersAndOrdering.Filters {

    public class LoggerUtil {

        public static void WriteLog(
            string loggerName, string loggerMethodName, 
            string controllerName, string actionName) {

            var logFormat = 
                "{0}, {1} method for Controller {2}, Action {3} is running...";

            Trace.TraceInformation(
                logFormat,
                loggerName, 
                loggerMethodName, 
                actionName, 
                controllerName);
        }
    }
}