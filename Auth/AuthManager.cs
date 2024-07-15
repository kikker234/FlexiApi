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
    
    public bool Register(string email, string password, Organization organization)
    {
        User user = new User
        {
            Email = email,
            UserName = email,
            Organization = organization
        };

        IdentityResult result = _userManager.CreateAsync(user, password).Result;

        if (result.Errors.Any())
        {
            throw new Exception(result.Errors.First().Description);
        }

        return result.Succeeded;
    }

    public async Task<string?> Login(string email, string password)
    {
        User? user = await _userManager.FindByEmailAsync(email);
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

        token = token.Replace("Bearer ", "");
        string? userId = TokenUtils.GetUserIdFromToken(token);
        
        return userId == null ? null : _userManager.FindByIdAsync(userId).Result;
    }

    public User? GetLoggedInUser(string email, string password)
    {
        return _userManager.FindByEmailAsync(email).Result;
    }

    public bool IsValidToken(string token)
    {
        return TokenUtils.IsValidToken(token);
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