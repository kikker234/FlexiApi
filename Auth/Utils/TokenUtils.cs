using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Logging;
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
    
    public static String? GetUserIdFromToken(string? token)
    {
        if (token == null)
        {
            return null;
        }

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

            string userId = jwtToken.Claims.First(x => x.Type == "user").Value;

            return userId;
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while validating token: " + e.Message);
            return null;
        }
    }
    
    public static JwtSecurityToken? GetJwtToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidIssuer = Issuer,
                ValidAudience = Audience,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);
        
            return (JwtSecurityToken)validatedToken;
        }
        catch (Exception e)
        {
            Console.WriteLine("An error occurred while validating token: " + e.Message);
            return null;
        }
    }

    public static bool IsValidToken(string? token)
    {
        if (token == null) return false;
        
        JwtSecurityToken? jwtToken = GetJwtToken(token);
        
        return jwtToken != null;
    }
}