using System.IdentityModel.Tokens.Jwt;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ActionExecutedContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext;
using ActionExecutingContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;
using IActionFilter = Microsoft.AspNetCore.Mvc.Filters.IActionFilter;

namespace Auth.Attributes;

public class AuthorizeActionFilter : IActionFilter
{
    private readonly FlexiContext _context;
    private readonly UserManager<User> _userManager;
        
    public AuthorizeActionFilter(FlexiContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.ActionDescriptor;
        var methodInfo = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)action).MethodInfo;
        var attribute = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false).FirstOrDefault() as AuthorizeAttribute;
        
        if (attribute == null) return;
        if (context.HttpContext.Request == null || context.HttpContext.Request.Headers == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        string? token = context.HttpContext.Request.Headers["Authorization"];
        
        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        token = token.Replace("Bearer ", "");

        JwtSecurityToken? jwtToken = TokenUtils.GetJwtToken(token);
        
        if (jwtToken == null)
        {
            return;
        }
        
        string userId = jwtToken.Claims.First(x => x.Type == "user").Value;
        
        if (String.IsNullOrEmpty(userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        if (attribute.Roles != null)
        {
            User? user = _userManager.FindByIdAsync(userId).Result;
        
            if (user == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            foreach (string role in attribute.Roles)
            {
                if (!_userManager.IsInRoleAsync(user, role).Result)
                {
                    context.Result = new UnauthorizedResult();
                    return;
                }
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
}