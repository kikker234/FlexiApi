using System.Net;
using System.Web.Mvc;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using ActionExecutedContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext;
using ActionExecutingContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;
using IActionFilter = Microsoft.AspNetCore.Mvc.Filters.IActionFilter;

namespace Auth.Attributes;

public class AuthorizeActionFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.ActionDescriptor;
        var methodInfo = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)action).MethodInfo;
        var attribute = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false).FirstOrDefault() as AuthorizeAttribute;

        if (attribute == null) return;
        
        // get instanceId from the JWT token
        
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
}