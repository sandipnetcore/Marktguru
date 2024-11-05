using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;

namespace MarktguruAssignment.API.Logging
{
    public static class FileLoggerExtension
    {
        /// <summary>
        /// Implement the Custom Logger.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static ILoggingBuilder AddFileLogger(
            this ILoggingBuilder builder,
            Action<FileLoggingOptions> configure)
        {
            builder.AddFileLogger();
            builder.Services.Configure(configure);

            return builder;
        }

        private static ILoggingBuilder AddFileLogger(
        this ILoggingBuilder builder)
        {
            builder.AddConfiguration();

            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, FileLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <FileLoggingOptions, FileLoggerProvider>(builder.Services);

            return builder;
        }
    }
}
