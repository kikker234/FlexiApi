using System.IdentityModel.Tokens.Jwt;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ActionExecutedContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext;
using ActionExecutingContext = Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;
using IActionFilter = Microsoft.AspNetCore.Mvc.Filters.IActionFilter;

namespace Auth.Attributes;

public class AuthorizeActionFilter : IActionFilter
{
    private readonly ITokenUtils _tokenUtils;
    private readonly UserManager<User> _userManager;

    public AuthorizeActionFilter(UserManager<User> userManager, ITokenUtils tokenUtils)
    {
        _userManager = userManager;
        _tokenUtils = tokenUtils;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        AuthorizeAttribute? authorizeAttribute = GetAuthorizeAttribute(context);
        if (authorizeAttribute == null) return;

        string? token = GetAuthorizationToken(context);
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        JwtSecurityToken? jwtToken = _tokenUtils.GetJwtToken(token);
        if (jwtToken == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        string? userId = GetUserIdFromToken(jwtToken);
        if (string.IsNullOrEmpty(userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        if (!UserHasRequiredRoles(userId, authorizeAttribute.Roles))
        {
            context.Result = new UnauthorizedResult();
        }
    }

    private AuthorizeAttribute? GetAuthorizeAttribute(ActionExecutingContext context)
    {
        var actionDescriptor =
            context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;
        if (actionDescriptor == null) return null;

        var methodInfo = actionDescriptor.MethodInfo;
        return methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), false).FirstOrDefault() as AuthorizeAttribute;
    }

    private string? GetAuthorizationToken(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        if (request?.Headers == null) return null;

        if (!request.Headers.TryGetValue("Authorization", out var authorizationHeader)) return null;

        return authorizationHeader.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);
    }

    private string? GetUserIdFromToken(JwtSecurityToken jwtToken)
    {
        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "user");
        return userIdClaim?.Value;
    }

    private bool UserHasRequiredRoles(string userId, string[]? roles)
    {
        if (roles == null || roles.Length == 0) return true;

        var user = _userManager.FindByIdAsync(userId).Result;
        if (user == null) return false;

        foreach (var role in roles)
        {
            if (!_userManager.IsInRoleAsync(user, role).Result)
            {
                return false;
            }
        }

        return true;
    }


    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
}