using Business.Services;
using Data.Models;
using FeatureFlags;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[Route("/api/[controller]")]
public class OrganizationController : Controller
{
    private readonly OrganizationServices _organizationServices;

    public OrganizationController(OrganizationServices organizationServices)
    {
        _organizationServices = organizationServices;
    }
    
    [HttpGet]
    public IActionResult GetOrganizations()
    {
        return Ok(_organizationServices.GetAll());
    }
    
    [HttpPost]
    public IActionResult AddOrganization([FromBody] Organization organization)
    {
        if (!_organizationServices.Create(organization))
            return StatusCode(500, "Error while adding organization");
        
        return Ok("Organization added successfully");
    }
    
    [Feature("DeleteOrganization")]
    [HttpDelete]
    public IActionResult DeleteOrganization(int id)
    {
        if (!_organizationServices.Delete(id))
            return StatusCode(500, "Error while deleting organization");
        
        return Ok("Organization deleted successfully");
    }
}