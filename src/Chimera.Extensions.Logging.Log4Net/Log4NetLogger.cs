namespace Chimera.Extensions.Logging.Log4Net
{
    using System;
    using Microsoft.Extensions.Logging;
    using log4net;

    public class Log4NetLogger : ILogger
    {
        private ILog _log;

        public Log4NetLogger(ILog log)
        {
            _log = log;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
        }
    }
}
