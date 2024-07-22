using Business.Services;
using Data.Models;
using FlexiApi.Attributes;
using FlexiApi.InputModels;
using FlexiApi.Resources;
using FlexiApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace FlexiApi.Controllers;

[Route("api/[controller]")]
public class OrganizationController : Controller
{
    
    private OrganizationServices _organizationServices;
    private IStringLocalizer<ValidationMessages> _localizer;
    
    public OrganizationController(OrganizationServices organizationServices, IStringLocalizer<ValidationMessages> localizer)
    {
        _organizationServices = organizationServices;
        _localizer = localizer;
    }
    
    [HttpPost]
    [Validation(typeof(CreateOrganization))]
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

            // get instance key from header from the request
            String instanceKey = Request.Headers["Instance"];
            instanceKey = "79abd14c-63d9-4d31-a68c-fbc91280ad1a";
            
            /*if(Request.Headers["Instance"].Count == 0)
            {
                return BadRequest(ApiResponse<string>.Error(_localizer["InstanceKeyMissing"]));
            }*/
            
            if (!_organizationServices.CreateNewOrganisation(email, password, organization, instanceKey))
            {
                throw new Exception("Failed to create organization");
            }
            
            return Ok(ApiResponse<string>.Success("Successfully created organization"));
        } catch (Exception e)
        {
            // print stack trace
            return StatusCode(500, ApiResponse<string>.Error(e));
        }
    }
}