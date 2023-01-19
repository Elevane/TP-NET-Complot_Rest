namespace TP_Complot_Rest.Common.Helpers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var token = context.HttpContext.Items["User"];

        if (token == null)
        {
            // not logged in
            context.Result = new JsonResult(new { message = "Unauthorized", isSuccess = false }) { StatusCode = StatusCodes.Status401Unauthorized };
        }
    }
}