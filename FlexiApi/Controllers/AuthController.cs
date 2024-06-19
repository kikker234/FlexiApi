using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[Route("/api/[controller]")]
public class AuthController : Controller
{
    
    [HttpPost]
    public IActionResult Login(String email, String password)
    {
        Console.WriteLine("Email: " + email);
        Console.WriteLine("Password: " + password);
        
        return Unauthorized("Invalid credentials");
    }
    
}