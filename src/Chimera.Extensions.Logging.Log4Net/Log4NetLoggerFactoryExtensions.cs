namespace Chimera.Extensions.Logging.Log4Net
{
    using System;
    using Microsoft.Extensions.Logging;

    public static class Log4NetLoggerFactoryExtensions
    {
        public static ILoggerFactory AddLog4Net(this ILoggerFactory loggerFactory)
        {
            return AddLog4Net(loggerFactory, Log4NetSettings.Default);
        }

        public static ILoggerFactory AddLog4Net(this ILoggerFactory loggerFactory, Log4NetSettings settings)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            var container = new Log4NetContainer(settings);
            container.Initialize();

            loggerFactory.AddProvider(new Log4NetProvider(container));

            return loggerFactory;
        }
    }
}
