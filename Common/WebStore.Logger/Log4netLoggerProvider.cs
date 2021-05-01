using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4netLoggerProvider : ILoggerProvider
    {
        private readonly string _ConfigurationFile;
        private readonly ConcurrentDictionary<string, Log4netLogger> _Loggers = new();

        public Log4netLoggerProvider(string ConfigurationFile) => _ConfigurationFile = ConfigurationFile;

        public ILogger CreateLogger(string categoryName)
        {
            return _Loggers.GetOrAdd(categoryName, category =>
            {
                var xml = new XmlDocument();
                xml.Load(_ConfigurationFile);
                return new Log4netLogger(category, xml["log4net"]);
            });
        }

        public void Dispose()
        {
            _Loggers.Clear();
        }
    }
}
