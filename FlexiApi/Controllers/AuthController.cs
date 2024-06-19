using Auth;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class AuthController : ControllerBase
{
    
    private readonly UserManager<User> _userManager;
    
    public AuthController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login(String email, String password)
    {
        User? user = _userManager.FindByEmailAsync(email).Result;

        if (user == null)
        {
            return BadRequest("Credentials not found");
        }

        PasswordHasher<User> ph = new PasswordHasher<User>();

        if (ph.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Failed)
        {
            return BadRequest("Credentials not found");
        }

        return Ok(TokenProvider.GenerateJwtToken(user.Id));
    }
    
}