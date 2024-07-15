using System.Collections;
using System.Text;
using System.Text.Json.Nodes;
using Auth;
using Auth.Attributes;
using Business.Services;
using Data;
using Data.Models;
using Data.Models.components;
using FlexiApi.InputModels;
using FlexiApi.Utils;
using Microsoft.AspNetCore.Mvc;
namespace FlexiApi.Controllers;

public class ComponentController : Controller
{
    private readonly ComponentServices _componentServices;
    private readonly IAuthManager _authManager;
    private readonly FlexiContext _context;
    
    public ComponentController(FlexiContext context, ComponentServices componentServices, IAuthManager authManager)
    {
        _componentServices = componentServices;
        _authManager = authManager;
        _context = context;
    }
    
    [HttpGet]
    [Authorize]
    [Route("/api/{type}")]
    public IActionResult GetComponents(string type)
    {
        try
        {
            User? user = _authManager.GetLoggedInUser(HttpContext);
            
            if(user == null) 
                throw new Exception("Could not load components!");

            Dictionary<string, string> body = new Dictionary<string, string>();
            return Ok(ApiResponse<IEnumerable<Component>>.Success(_componentServices.GetComponents(user, type, body)));
        }
        catch (Exception e)
        {
            return StatusCode(500, "Could not load components!");
        }
    }
    
    [HttpPost]
    [Authorize]
    [Route("/api/{type}")]
    public async Task<IActionResult> CreateComponent(string type, [FromBody] ComponentDto componentDto)
    {
        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null) return new UnauthorizedResult();
        
        try
        {
            Component component = new Component();
            component.Name = componentDto.Name;
            component.Type = componentDto.Type;
            component.OrganizationId = user.OrganizationId;
            
            IList<ComponentField> fields = new List<ComponentField>();
            foreach (var field in componentDto.Fields)
            {
                ComponentField componentField = new();
                componentField.Key = field.Key;
                fields.Add(componentField);
            }
            
            component.CustomComponentFields = fields;
            
            _context.Components.Add(component);
            _context.SaveChanges();
            
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "Could not create component!");
        }
    }     
}