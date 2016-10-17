using System;
using System.Web.Http;
using System.Web.Http.Tracing;
using WebApiConsoleTracing.Tracers;

namespace WebApiConsoleTracing.App_Start {
    public static class TraceConfig {
        public static void Register(HttpConfiguration configuration) {
            if (configuration == null) {
                throw new ArgumentNullException("configuration");
            }

            var traceWriter = new ConsoleTraceWriter();
            configuration.Services.Replace(typeof (ITraceWriter), traceWriter);
        }
    }
}