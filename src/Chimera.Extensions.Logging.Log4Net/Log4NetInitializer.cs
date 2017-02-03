namespace Chimera.Extensions.Logging.Log4Net
{
    using System;
    using System.IO;
    using log4net;
    using log4net.Config;
    using log4net.Repository;

    public class Log4NetInitializer : ILog4NetInitializer
    {
        private Log4NetSettings _settings;
        private ILoggerRepository _loggerRepository;

        public bool IsInitialized
        {
            get { return _loggerRepository != null; }
        }

        public ILoggerRepository LoggerRepository
        {
            get { return _loggerRepository; }
        }

        public Log4NetInitializer(Log4NetSettings settings)
        {
            if (settings == null)
            {
                throw new ArgumentNullException(nameof(settings));
            }

            _settings = settings;
        }

        public void Initialize()
        {
            if (IsInitialized) return;

            foreach (var item in _settings.GlobalContextProperties)
            {
                GlobalContext.Properties[item.Key] = item.Value;
            }
            
            _loggerRepository = CreateRootRepository();
        }

        private ILoggerRepository CreateRootRepository()
        {
            var loggerRepository = LogManager.CreateRepository(_settings.RootRepositoryName);

            var fileInfo = new FileInfo(Path.GetFullPath(_settings.ConfigFilePath));
            if (_settings.Watch)
            {
                XmlConfigurator.ConfigureAndWatch(loggerRepository, fileInfo);
            }
            else
            {
                XmlConfigurator.Configure(loggerRepository, fileInfo);
            }

            return loggerRepository;
        }
    }
}