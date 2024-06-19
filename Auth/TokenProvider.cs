using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Auth;

public class TokenProvider
{
    public static string? SecretKey { get; set; } = null;
    public static string? Issuer { get; set; } = null;
    public static string? Audience { get; set; } = null;

    public static string GenerateJwtToken(string userId)
    {
        if (SecretKey == null || Issuer == null || Audience == null)
            throw new Exception("SecretKey, Issuer and Audience must be set in TokenProvider.cs");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("user", userId.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(12),
            Issuer = Issuer,
            Audience = Audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}