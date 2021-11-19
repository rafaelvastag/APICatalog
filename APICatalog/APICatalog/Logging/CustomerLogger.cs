using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalog.Logging
{
    public class CustomerLogger : ILogger
    {

        readonly string LoggerName;
        readonly CustomLoggerProviderConfiguration LoggerConfig;

        public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
        {
            LoggerName = name;
            LoggerConfig = config;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel == LoggerConfig.LogLevel;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";

            WriteMessageInFile(message);
        }

        private void WriteMessageInFile(string message)
        {
            string LogFilePath = @"C:\logger\log.txt";
            using (StreamWriter sw = new StreamWriter(LogFilePath,true))
            {
                try
                {
                    sw.WriteLine(message);
                    sw.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
