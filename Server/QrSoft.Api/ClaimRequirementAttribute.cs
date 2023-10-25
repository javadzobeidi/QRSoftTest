using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Security.Claims;

public class ClaimRequirementAttribute : TypeFilterAttribute
{
    public ClaimRequirementAttribute(params string[] permissions) : base(typeof(ClaimAuthorizationFilter))
    {
        Arguments = new object[] { permissions };
    }

    private class ClaimAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string[] _permissions;

        public ClaimAuthorizationFilter(string[] permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            if (!user.Identity.IsAuthenticated)
            {
                context.Result = new CustomUnauthorizedResult("Authorization failed.");
                return;
            }

            var currentClaim = user.Claims.Where(d => d.Type == ClaimTypes.Role && _permissions.Contains(d.Value) == true).FirstOrDefault();

            if (currentClaim ==null)
            {
                context.Result = new CustomUnAuthenticatedResult("Authenticated failed.");
                    return;
             }


    }


}

    public class CustomUnauthorizedResult : JsonResult
    {
        public CustomUnauthorizedResult(string message)
            : base(new CustomError(message))
        {
            StatusCode = StatusCodes.Status401Unauthorized;
        }
    }

    public class CustomUnAuthenticatedResult : JsonResult
    {
        public CustomUnAuthenticatedResult(string message)
            : base(new CustomError(message))
        {
            StatusCode = StatusCodes.Status403Forbidden;
        }
    }



    public class CustomError
    {
        public string Error { get; }
        public CustomError(string message)
        {
            Error = message;
        }
    }
}



