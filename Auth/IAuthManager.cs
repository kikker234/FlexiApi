using Data.Models;
using Microsoft.AspNetCore.Http;

namespace Auth;

public interface IAuthManager
{
    bool Register(string email, string password);
    Task<string?> Login(string email, string password);
    bool DisableAccount(string email, string password);
    User? GetLoggedInUser(HttpContext context);
    User? GetLoggedInUser(string email, string password);
    bool IsValidToken(string token);
}