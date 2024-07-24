using Auth;
using Auth.Attributes;
using Data.Models;
using FlexiApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : Controller
{
    
    private readonly IAuthManager _authManager;
    
    public AuthController(IAuthManager authManager)
    {
        _authManager = authManager;
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
        return Ok();
        
        bool isValid = _authManager.IsValidToken(token);
        
        return Ok(ApiResponse<bool>.Success(isValid));
    }
    
}