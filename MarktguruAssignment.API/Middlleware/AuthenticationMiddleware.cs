using Marktguru.BusinessLogic.Configurations;
using Marktguru.BusinessLogic.Users;
using Microsoft.Extensions.Options;

namespace MarktguruAssignment.API.Middlleware
{
    /// <summary>
    /// Respomnsible for Authentication
    /// </summary>
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        
        private JWTConfigurationSettings _jWTSettings { get; }
        
        public AuthenticationMiddleware(RequestDelegate next, IOptions<JWTConfigurationSettings> config)
        {
            _next = next;
            _jWTSettings = config.Value;
        }

        public Task Invoke(HttpContext httpContext)
        {
            //We have to inform the audience to pass the token in the Request Headers.
            if(!httpContext.Request.Headers.ContainsKey("token"))
            {
                return _next(httpContext);
            }

            var requestToken = httpContext.Request.Headers["token"].ToString();

            UserRepository userRepository = new UserRepository(_jWTSettings);

            try
            {
                var validatedToken = userRepository.GetTokenOnValidation(requestToken);

                if (validatedToken == null)
                {
                    return _next(httpContext);
                }

                var user = validatedToken.Claims.FirstOrDefault(c => c.Type == "UserName");

                if (user == null)
                {
                    return _next(httpContext);
                }

                //if user is not null then we can add the user to the context. Meaning its a valid token and we can use it for Authorization 
                httpContext.Items["User"] = user;
            }
            catch (Exception ex) 
            {
                throw;
            }
            return _next(httpContext);
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
