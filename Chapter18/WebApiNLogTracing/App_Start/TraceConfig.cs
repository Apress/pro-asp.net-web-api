using System;
using System.Web.Http;
using System.Web.Http.Tracing;
using WebApiNLogTracing.TraceWriters;

namespace WebApiNLogTracing.App_Start {
public static class TraceConfig {
    public static void Register(HttpConfiguration configuration) {
        if (configuration == null) {
            throw new ArgumentNullException("configuration");
        }

        var traceWriter =
            new NLogTraceWriter();

        configuration.Services.Replace(typeof (ITraceWriter), traceWriter);
    }
}
}