using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Auth;

public class AuthManager : IAuthManager
{
    private readonly UserManager<User> _userManager;
    
    public AuthManager(UserManager<User> userManager)
    {
        _userManager = userManager;
    }
    
    public bool Register(int organization, string email, string password)
    {
        User user = new User
        {
            Email = email,
            UserName = email,
            OrganizationId = organization,
        };

        IdentityResult result = _userManager.CreateAsync(user, password).Result;

        if (result.Errors.Any())
        {
            throw new Exception(result.Errors.First().Description);
        }

        return result.Succeeded;
    }

    public string? Login(string email, string password)
    {
        User? user = _userManager.FindByEmailAsync(email).Result;
        if (user == null) return null;

        return TokenUtils.GenerateJwtToken(user.Id);
    }

    public bool DisableAccount(string email, string password)
    {
        User? user = CanLogin(email, password);
        if (user == null) return false;

        IdentityResult result = _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue).Result;
        return result.Succeeded;
    }

    public User? GetLoggedInUser(HttpContext context)
    {
        string? token = context.Request.Headers["Authorization"];
        if (token == null) return null;

        string userId = TokenUtils.GetUserIdFromToken(token);
        
        return _userManager.FindByIdAsync(userId).Result;
    }

    private User? CanLogin(string email, string password)
    {
        User? user = _userManager.FindByEmailAsync(email).Result;

        if(user == null) return null;
        
        PasswordHasher<User> ph = new PasswordHasher<User>();

        if (ph.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
            return null;
        
        return user;
    }
}