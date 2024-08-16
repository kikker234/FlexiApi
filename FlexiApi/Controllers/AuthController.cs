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
        String? token = await _authManager.Login(email, password);

        if (token == null)
            return BadRequest(ApiResponse<string>.Error("Invalid credentials"));

        
        return Ok(ApiResponse<string>.Success(token));
    }
    
    [HttpGet]
    [Route("valid")]
    public IActionResult ValidateToken(String token)
    {
        _logger.Information("Checking token validity");
        bool isValid = _authManager.IsValidToken(token);

        _logger.Information("Token is valid: {isValid}", isValid);
        _logger.Fatal(":NM_peepoRedAlarm:");
        
        return Ok(ApiResponse<bool>.Success(isValid));
    }
    
}