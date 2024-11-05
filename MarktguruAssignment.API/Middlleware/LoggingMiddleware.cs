using Marktguru.BusinessLogic.Users;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MarktguruAssignment.API.Middlleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    /// <summary>
    /// Logg any errors with the incoming request or outgoing responses;
    /// </summary>
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                //Only Errors should have stack trace or the log files will be too huge.
                var userName = string.Empty;
                if (httpContext.Items["User"] != null)
                {
                    userName = ((Claim)httpContext.Items["User"]).Value;
                }
                _logger.LogError(string.Format("{0}{1}", userName, ex.StackTrace));
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await httpContext.Response.WriteAsJsonAsync("Internal Server Error");
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggingMiddleware>();
        }
    }
}
