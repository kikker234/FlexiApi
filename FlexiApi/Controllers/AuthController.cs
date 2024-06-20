using Auth;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
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
            return BadRequest("Credentials not found");

        return Ok(token);
    }
    
    [HttpPost]
    public IActionResult Register(String email, String password)
    {
        if (!_authManager.Register(email, password))
            return BadRequest("Could not create account");

        return Ok();
    }
    
    [HttpDelete]
    public IActionResult Disable(String email, String password)
    {
        if (!_authManager.DisableAccount(email, password))
            return BadRequest("User not found");

        return Ok();
    }
    
}