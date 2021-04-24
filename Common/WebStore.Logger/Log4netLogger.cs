using log4net;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Xml;

namespace WebStore.Logger
{
    public class Log4netLogger : ILogger
    {
        private readonly ILog _Log;
        public Log4netLogger(string Category, XmlElement Configuration)
        {
            var loggerRepository = LogManager.CreateRepository(
                Assembly.GetEntryAssembly(),
                typeof(log4net.Repository.Hierarchy.Hierarchy));

            _Log = LogManager.GetLogger(loggerRepository.Name, Category);

            log4net.Config.XmlConfigurator.Configure(loggerRepository, Configuration);
        }
        public IDisposable BeginScope<TState>(TState state) => null;

        public bool IsEnabled(LogLevel logLevel) => logLevel switch
        {
            LogLevel.None => false,
            LogLevel.Trace => _Log.IsDebugEnabled,
            LogLevel.Debug => _Log.IsDebugEnabled,
            LogLevel.Information => _Log.IsInfoEnabled,
            LogLevel.Warning => _Log.IsWarnEnabled,
            LogLevel.Error => _Log.IsErrorEnabled,
            LogLevel.Critical => _Log.IsFatalEnabled,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };

        public void Log<TState>(
            LogLevel Level, 
            EventId Id, 
            TState State, 
            Exception Error, Func<TState, Exception, string> Formatter)
        {
            if (Formatter is null) throw new ArgumentNullException(nameof(Formatter));

            var logMessage = Formatter(State, Error);

            if (string.IsNullOrEmpty(logMessage) && Error is null) return;

            switch (Level)
            {
                default: throw new ArgumentOutOfRangeException(nameof(Level), Level, null);
                case LogLevel.None:
                    break;
                case LogLevel.Trace:
                case LogLevel.Debug:
                    _Log.Debug(logMessage);
                    break;
                case LogLevel.Information:
                    _Log.Info(logMessage);
                    break;
                case LogLevel.Warning:
                    _Log.Warn(logMessage);
                    break;
                case LogLevel.Error:
                    _Log.Error(logMessage, Error);
                    break;
                case LogLevel.Critical:
                    _Log.Fatal(logMessage, Error);
                    break;
            }
        }
    }
}
