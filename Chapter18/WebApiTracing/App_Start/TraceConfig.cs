using System;
using System.Web.Http;
using System.Web.Http.Tracing;

namespace WebApiTracing.App_Start {
    public static class TraceConfig {
        public static void Register(HttpConfiguration configuration) {
            if (configuration == null) {
                throw new ArgumentNullException("configuration");
            }

            var traceWriter =
                new SystemDiagnosticsTraceWriter() {
                                                       MinimumLevel = TraceLevel.Info,
                                                       IsVerbose = false
                                                   };

            configuration.Services.Replace(typeof (ITraceWriter), traceWriter);
        }
    }
}