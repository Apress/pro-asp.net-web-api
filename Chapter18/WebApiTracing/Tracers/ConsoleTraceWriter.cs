using System;
using System.Net.Http;
using System.Web.Http.Tracing;

namespace WebApiConsoleTracing.Tracers {
    public class ConsoleTraceWriter : ITraceWriter {
        public void Trace(HttpRequestMessage request, string category,
                          TraceLevel level, Action<TraceRecord> traceAction) {
            var traceRecord = new TraceRecord(request, category, level);
            traceAction(traceRecord);
            ShowTrace(traceRecord);
        }

        private void ShowTrace(TraceRecord traceRecord) {
            Console.WriteLine(
                "{0} {1}: Category={2}, Level={3} {4} {5} {6} {7}",
                traceRecord.Request.Method.ToString(),
                traceRecord.Request.RequestUri.ToString(),
                traceRecord.Category,
                traceRecord.Level,
                traceRecord.Kind,
                traceRecord.Operator,
                traceRecord.Operation,
                traceRecord.Exception != null
                    ? traceRecord.Exception.GetBaseException().Message
                    : !string.IsNullOrEmpty(traceRecord.Message)
                          ? traceRecord.Message
                          : string.Empty
                );
        }
    }
}