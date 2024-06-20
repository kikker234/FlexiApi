using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
        if (context.HttpContext.Request == null || context.HttpContext.Request.Headers == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        string token = context.HttpContext.Request.Headers["Authorization"];

        if (token == null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
        
        token = token.Replace("Bearer ", "");
        int? userId = ValidateToken(token);
        
        if (userId != null)
        {
            context.Result = new UnauthorizedResult();
            return;
        }
    }

    public void OnActionExecuted(ActionExecutedContext filterContext)
    {
    }
    
    public int? ValidateToken(string token)
    {
        if (token == null) 
            return null;

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "7b5c2a8dbf9c-4a049ec8f8611a9d49097b5c2a8dbf9c4a049ec8f8611a9d4909"u8.ToArray();
        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

            // return user id from JWT token if validation successful
            return userId;
        }
        catch
        {
            // return null if validation fails
            return null;
        }
    }
}