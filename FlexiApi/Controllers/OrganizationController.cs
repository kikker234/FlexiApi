using Business.Services;
using Data.Models;
using FlexiApi.InputModels;
using FlexiApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[Route("api/[controller]")]
public class OrganizationController : Controller
{
    
    private OrganizationServices _organizationServices;
    
    public OrganizationController(OrganizationServices organizationServices)
    {
        _organizationServices = organizationServices;
    }
    
    [HttpPost]
    public IActionResult CreateOrganization([FromBody] CreateOrganization data)
    {
        try
        {
            String email = data.Email;
            String password = data.Password;
            String organizationName = data.OrganizationName;
    

            Organization organization = new Organization
            {
                Name = organizationName
            };
            
            _organizationServices.CreateNewOrganisation(email, password, organization);
            
            return Ok(ApiResponse<string>.Success("Successfully created organization"));
        } catch (Exception e)
        {
            return StatusCode(500, ApiResponse<string>.Error(e.Message));
        }
    }
}