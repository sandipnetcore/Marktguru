using Marktguru.BusinessLogic.Users;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace MarktguruAssignment.API.Attributes
{
    /// <summary>
    /// Manage the authorization with this filter.
    /// We are using filter because it is required only by the ProductController.
    /// Else we can use it globally in the middleware.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<string> _roles;
        public AuthorizeAttribute(params string[] Roles)
        {
            _roles = Roles ?? new string[] { };
        }

        /// <inheritdoc/>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.Items["User"];

            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized. The token is not valid" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
