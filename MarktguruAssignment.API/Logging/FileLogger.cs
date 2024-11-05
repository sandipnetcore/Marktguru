using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktguruAssignment.API.Logging
{
    /// <summary>
    /// Implementing the Custom Logger
    /// </summary>
    public class FileLogger: ILogger
    {
        private FileLoggerProvider _FileLoggerProvider {  get; set; }
        public FileLogger(FileLoggerProvider provider) 
        {
            _FileLoggerProvider = provider;
        }

        /// <inheritdoc/>
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!IsEnabled(logLevel)) 
            {
                return;
            }


            var filePath = Path.Combine(_FileLoggerProvider._LoggingOptions.LogFolderPath, 
                                        _FileLoggerProvider._LoggingOptions.LogFilePath.
                                            Replace(_FileLoggerProvider._LoggingOptions.ReplaceString, DateTime.UtcNow.ToString("ddMMMyyyy")));

            var logMessage = String.Format("{0} [{1}] {2} {3}", 
                                                    DateTime.UtcNow.ToString("dd-MMM-yyyy HH:mm:ss"), 
                                                    logLevel.ToString(), 
                                                    formatter(state, exception), 
                                                    exception != null ? exception.StackTrace : "");

            using (var sw = new StreamWriter(filePath, true)) 
            {
                sw.WriteLine(logMessage);
            }
        
        }

        /// <inheritdoc/>
        public bool IsEnabled(LogLevel logLevel)
        {
            //We dont need, hence we just keep everything other than NONE.
            return logLevel != LogLevel.None;
        }

        /// <inheritdoc/>
        public IDisposable? BeginScope<TState>(TState state) where TState : notnull
        {
            return null;
        }
    }
}
