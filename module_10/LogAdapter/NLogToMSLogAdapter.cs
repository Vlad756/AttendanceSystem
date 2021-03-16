using Microsoft.Extensions.Logging;
using System;

namespace LogAdapter
{
    public class NLogToMSLogAdapter : ILogger
    {
        private readonly NLog.ILogger _logger;

        private static NLogToMSLogAdapter _adapter;

        private NLogToMSLogAdapter(NLog.ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public static NLogToMSLogAdapter GetAdapter(NLog.ILogger logger)
        {
            if (_adapter == null)
            {
                _adapter = new NLogToMSLogAdapter(logger);
            }

            return _adapter;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            if (state == null)
            {
                throw new ArgumentNullException(nameof(state));
            }

            return NLog.NestedDiagnosticsLogicalContext.Push(state);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _logger.IsEnabled(ConvertLogLevel(logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            _logger.Log<string>(ConvertLogLevel(logLevel), formatter(state, exception));
        }

        private NLog.LogLevel ConvertLogLevel(LogLevel MsLogLevel)
        {
            NLog.LogLevel level = MsLogLevel switch
            {
                LogLevel.Trace => NLog.LogLevel.Trace,
                LogLevel.Debug => NLog.LogLevel.Debug,
                LogLevel.Information => NLog.LogLevel.Info,
                LogLevel.Warning => NLog.LogLevel.Warn,
                LogLevel.Error => NLog.LogLevel.Error,
                LogLevel.Critical => NLog.LogLevel.Fatal,
                LogLevel.None => NLog.LogLevel.Off,
                _ => throw new ArgumentException(nameof(MsLogLevel)),
            };

            return level;
        }
    }
}
