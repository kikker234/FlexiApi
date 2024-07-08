using Business.Services;
using Data.Models;
using FlexiApi.Attributes;
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
    [Validation(typeof(CreateOrganization))]
    public IActionResult CreateOrganization([FromBody] CreateOrganization data)
    {
        if (!ModelState.IsValid)
        {
            Console.WriteLine(data);
            Dictionary<string, string> errors = new();
            
            foreach (var (key, value) in ModelState)
            {
                errors.Add(key, value.Errors.First().ErrorMessage);
                Console.WriteLine("Adding error for: " + key);
            }
            
            return BadRequest(errors);
        }
        
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
            Console.WriteLine(instanceKey);
            
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