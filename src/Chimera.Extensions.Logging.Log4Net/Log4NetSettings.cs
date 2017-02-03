namespace Chimera.Extensions.Logging.Log4Net
{
    using System.IO;
    using System.Collections.Generic;

    public class Log4NetSettings
    {
        public static Log4NetSettings Default = new Log4NetSettings();
        
        private IDictionary<string, object> _contextProperties = new Dictionary<string, object>();

        public IDictionary<string, object> GlobalContextProperties
        {
            get { return _contextProperties; }
        }

        public string ConfigFilePath { get; set; }

        public string RootRepositoryName { get; set; }

        public bool Watch { get; set; }

        public Log4NetSettings()
        {
            Default.GlobalContextProperties.Add("appRoot", Directory.GetCurrentDirectory());
            Default.ConfigFilePath = "log4net.config";
            Default.RootRepositoryName = GetDefaultRepositoryName();
        }

        private static string GetDefaultRepositoryName()
        {
#if NETSTANDARD1_3
            return "Root";
#else
            return System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
#endif
        }
    }
}
