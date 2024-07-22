using System.IdentityModel.Tokens.Jwt;

namespace Auth;

public interface ITokenUtils
{
    string GenerateJwtToken(string userId);
    String? GetUserIdFromToken(string? token);
    JwtSecurityToken? GetJwtToken(string token);
    bool IsValidToken(string? token);
}