namespace Chimera.Extensions.Logging.Log4Net
{
    using log4net.Repository;

    public interface ILog4NetContainer
    {
        bool IsInitialized { get; }

        ILoggerRepository LoggerRepository { get; }

        void Initialize();
    }
}