namespace Chimera.Extensions.Logging.Log4Net
{
    public interface ILog4NetInitializer
    {
        bool IsInitialized { get; }

        void Initialize();
    }
}