using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[Route("/api/[controller]")]
public class CustomerController : Controller
{
    [HttpGet("{organisationId:int}")]
    public IActionResult GetCustomers(int organisationId)
    {
        if(organisationId == 0)
            return BadRequest("Invalid Organisation Id");
        
        if(organisationId == 2)
            return NotFound("Organisation not found");
        
        return Ok(new { Name = "John Doe", Age = 32, OrganisationId = organisationId });
    }
}