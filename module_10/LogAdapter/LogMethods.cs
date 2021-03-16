using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text;

namespace LogAdapter
{
    public abstract class LogMethods
    {
        public static void LogErrorWithCustomStackTrace(string message, Exception ex, ILogger logger)
        {
            var sb = new StringBuilder();
            var st = new StackTrace(true);

            var sf = st.GetFrames();

            if (!string.IsNullOrEmpty(message))
            {
                sb.Append(message);
            }

            sb.Append("StackTrace: ");

            for (var i = 1; i < sf.Length; i++)
            {
                var line = (i != 1) ? $"(line {sf[i].GetFileLineNumber()})" : string.Empty;
                var className = sf[i].GetMethod().ReflectedType.Name;
                var methodName = sf[i].GetMethod().Name;

                sb.Append($"{line} {className}.{methodName} => ");
            }

            LogError(sb.ToString(), ex, logger);
        }

        public static string Formatter<TState>(TState state, Exception exception)
        {
            var sb = new StringBuilder();

            if (!string.IsNullOrEmpty(state?.ToString()))
            {
                sb.Append(state.ToString());
                sb.Append(" ");
            }

            if (exception != null)
            {
                sb.Append(exception.ToString());
            }

            return sb.ToString();
        }

        public static void LogError(string message, Exception exception, ILogger logger)
        {
            logger?.Log(LogLevel.Error, new EventId(), message, exception, Formatter);
        }

        public static void LogOther(LogLevel level, string message, ILogger logger)
        {
            logger?.Log(level, new EventId(), message, null, Formatter);
        }
    }
}
