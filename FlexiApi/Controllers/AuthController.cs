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
    public IActionResult Login(String email, String password)
    {
        String? token = _authManager.Login(email, password);

        if (token == null)
            return BadRequest(ApiResponse<string>.Error("Invalid credentials"));

        return Ok(ApiResponse<string>.Success(token));
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult Register(String email, String password)
    {
        try
        {
            HttpContext context = HttpContext;
            User user = _authManager.GetLoggedInUser(context);
            
            _authManager.Register(email, password);
            return Ok(ApiResponse<string>.Success("User registered successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<String>.Error(e));
        }
    }
    
}