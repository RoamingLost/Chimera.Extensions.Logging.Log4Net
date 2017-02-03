namespace Chimera.Extensions.Logging.Log4Net
{
    using log4net.Repository;

    public interface ILog4NetInitializer
    {
        bool IsInitialized { get; }

        ILoggerRepository LoggerRepository { get; }

        void Initialize();
    }
}