using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.Http.Tracing;
using NLog;

namespace WebApiNLogTracing.TraceWriters {
    public class NLogTraceWriter : ITraceWriter {
private static readonly Lazy<Dictionary<TraceLevel, Action<string>>> Loggers =
    new Lazy<Dictionary<TraceLevel, Action<string>>>(() => 
        new Dictionary<TraceLevel, Action<string>> {
            { TraceLevel.Debug, LogManager.GetCurrentClassLogger().Debug }, 
            { TraceLevel.Error, LogManager.GetCurrentClassLogger().Error }, 
            { TraceLevel.Fatal, LogManager.GetCurrentClassLogger().Fatal }, 
            { TraceLevel.Info, LogManager.GetCurrentClassLogger().Info }, 
            { TraceLevel.Warn, LogManager.GetCurrentClassLogger().Warn }
        });

private Dictionary<TraceLevel, Action<string>> currentLogger {
    get { return Loggers.Value; }
}

    public void Trace(
        HttpRequestMessage request, 
        string category, 
        TraceLevel level, 
        Action<TraceRecord> traceAction) {
            if (level == TraceLevel.Off) return;
            var record = new TraceRecord(request, category, level);
            traceAction(record);
            logToNLog(record);
    }

private void logToNLog(TraceRecord traceRecord) {
    var messageBuilder = new StringBuilder();

    if (traceRecord.Request != null) {
        if (traceRecord.Request.Method != null) {
            messageBuilder.Append(" " + traceRecord.Request.Method);
        }

        if (traceRecord.Request.RequestUri != null) {
            messageBuilder.Append(" " + traceRecord.Request.RequestUri);
        }
    }

    if (!string.IsNullOrWhiteSpace(traceRecord.Category)) {
        messageBuilder.Append(" " + traceRecord.Category);
    }

    if (!string.IsNullOrWhiteSpace(traceRecord.Operator)) {
        messageBuilder.Append(" " + traceRecord.Operator + " " + traceRecord.Operation);
    }

    if (!string.IsNullOrWhiteSpace(traceRecord.Message)) {
        messageBuilder.Append(" " + traceRecord.Message);
    }

    if (traceRecord.Exception != null) {
        messageBuilder.Append(traceRecord.Exception.GetBaseException().Message);
    }

    currentLogger[traceRecord.Level](messageBuilder.ToString());
}
    }
}