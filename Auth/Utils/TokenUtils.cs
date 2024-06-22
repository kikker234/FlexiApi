using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data.Models;
using Microsoft.IdentityModel.Tokens;

namespace Auth;

public class TokenUtils
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

    public static String GetUserIdFromToken(string token)
    {
        if(token.StartsWith("Bearer "))
            token = token.Substring(token.IndexOf("Bearer ") + 7);
        
        if (SecretKey == null)
            throw new Exception("SecretKey must be set in TokenProvider.cs");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = Issuer,
            ValidAudience = Audience,
            ValidateLifetime = true
        };

        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);

        var userId = principal.FindFirst("user")?.Value;
        return userId ?? throw new Exception("User not found in token");
    }
}