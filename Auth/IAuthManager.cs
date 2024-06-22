using Data.Models;
using Microsoft.AspNetCore.Http;

namespace Auth;

public interface IAuthManager
{
    bool Register(int organization, string email, string password);
    string? Login(string email, string password);
    bool DisableAccount(string email, string password);
    User GetLoggedInUser(HttpContext context);
}