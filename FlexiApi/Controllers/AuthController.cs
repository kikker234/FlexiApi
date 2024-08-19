using Auth;
using FlexiApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Serilog.Extensions.Logging;

namespace FlexiApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : Controller
{
    private readonly IAuthManager _authManager;
    private readonly Serilog.ILogger _logger;

    public AuthController(IAuthManager authManager, Serilog.ILogger logger)
    {
        _authManager = authManager;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Login(String email, String password)
    {
        _logger.Information("Logging in user with email: {email}", email);
        String? token = await _authManager.Login(email, password);

        if (token == null)
        {
            _logger.Warning("Invalid login attempt for email: {Email}", email);
            return Unauthorized(ApiResponse<string>.Error("Invalid credentials"));
        }

        _logger.Information("User logged in successfully with email: {email}", email);
        return Ok(ApiResponse<string>.Success(token));
    }

    [HttpGet]
    [Route("valid")]
    public IActionResult ValidateToken(String token)
    {
        _logger.Information("Checking token validity: {token}", token);
        bool isValid = _authManager.IsValidToken(token);

        if (!isValid)
        {
            _logger.Warning("Invalid token: {token}", token);
            return Unauthorized(ApiResponse<bool>.Error("Invalid token"));
        }

        _logger.Information("Token is valid: {token}", token);
        return Ok(ApiResponse<bool>.Success(isValid));
    }
}