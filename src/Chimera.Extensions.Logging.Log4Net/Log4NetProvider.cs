namespace Chimera.Extensions.Logging.Log4Net
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.Logging;

    public class Log4NetProvider : ILoggerProvider
    {
        private IDictionary<string, ILogger> _loggers = new Dictionary<string, ILogger>();

        public ILogger CreateLogger(string categoryName)
        {
            if (!_loggers.ContainsKey(categoryName))
            {
                lock (_loggers)
                {
                    if (!_loggers.ContainsKey(categoryName))
                    {
                        _loggers[categoryName] = new Log4NetLogger();
                    }
                }
            }

            return _loggers[categoryName];
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                    _loggers.Clear();
                    _loggers = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Log4NetProvider() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
