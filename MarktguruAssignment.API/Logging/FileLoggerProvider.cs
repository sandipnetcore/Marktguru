using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarktguruAssignment.API.Logging
{
    /// <summary>
    /// Provider for File Logger
    /// </summary>
    [ProviderAlias("FileLogger")]
    public class FileLoggerProvider: ILoggerProvider
    {
        internal readonly FileLoggingOptions _LoggingOptions;
        
        public FileLoggerProvider([NotNull] IOptions<FileLoggingOptions> LoggingOptions)
        {
            _LoggingOptions = LoggingOptions.Value;

            if(!Directory.Exists(_LoggingOptions.LogFolderPath))
            {
                Directory.CreateDirectory(_LoggingOptions.LogFolderPath);
            }
        }

        /// <inheritdoc/>
        public ILogger CreateLogger(string categoryName)
        {
            return new FileLogger(this);
        }


        /// <inheritdoc/>
        public void Dispose()
        {
            return;
        }
    }
}
