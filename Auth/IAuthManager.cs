namespace Auth;

public interface IAuthManager
{
    bool Register(string email, string password);
    string? Login(string email, string password);
    bool DisableAccount(string email, string password);
}