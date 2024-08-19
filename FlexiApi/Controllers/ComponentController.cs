using System.Collections;
using System.Text;
using System.Text.Json.Nodes;
using Auth;
using Auth.Attributes;
using Business;
using Business.Services;
using BusinessTest.Exceptions;
using Data;
using Data.Models;
using Data.Models.components;
using FlexiApi.InputModels;
using FlexiApi.Utils;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

public class ComponentController : Controller
{
    private ComponentServices _componentServices;
    private IAuthManager _authManager;
    private readonly Serilog.ILogger _logger;

    public ComponentController(IAuthManager authManager, ComponentServices componentServices, Serilog.ILogger logger)
    {
        _componentServices = componentServices;
        _authManager = authManager;
        _logger = logger;
    }

    [HttpGet]
    [Authorize]
    [Route("/api/{type}")]
    public IActionResult GetComponentsAsync(string type)
    {
        _logger.Information("Fetching components of type: {type}", type);

        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
        {
            _logger.Warning("Unauthorized access attempt to fetch components of type: {type}", type);
            return Unauthorized();
        }

        Instance instance = user.Organization.Instance;
        IEnumerable components = _componentServices.GetComponents(type, instance);

        _logger.Information("Successfully fetched components of type: {type} for user: {user}", type, user.Email);
        return Ok(components);
    }

    [HttpGet]
    [Authorize]
    [Route("/api/{type}/{id}")]
    public IActionResult GetComponent(string type, int id)
    {
        _logger.Information("Fetching component of type: {type} with ID: {id}", type, id);

        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
        {
            _logger.Warning("Unauthorized access attempt to fetch component of type: {type} with ID: {id}", type, id);
            return Unauthorized();
        }

        ComponentObject? component = _componentServices.GetComponent(id);
        if (component == null)
        {
            _logger.Warning("Component of type: {type} with ID: {id} not found", type, id);
            return NotFound();
        }

        _logger.Information("Successfully fetched component of type: {type} with ID: {id} for user: {user}", type, id, user.Email);
        return Ok(component);
    }

    [HttpPost]
    [Authorize]
    [Route("/api/{type}")]
    public IActionResult CreateComponent(string type, [FromBody] Dictionary<string, string> body)
    {
        _logger.Information("Creating component of type: {type}", type);

        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
        {
            _logger.Warning("Unauthorized access attempt to create component of type: {type}", type);
            return Unauthorized();
        }

        Instance instance = user.Organization.Instance;

        try
        {
            _componentServices.Create(user, instance, type, body);
            _logger.Information("Successfully created component of type: {type} for user: {user}", type, user.Email);
        }
        catch (ComponentNotFoundException e)
        {
            _logger.Warning("Component creation failed: {message}", e.Message);
            return NotFound(e.Message);
        }
        catch (InvalidFieldDataException e)
        {
            _logger.Warning("Component creation failed due to invalid data: {message}", e.Message);
            return BadRequest(e.Message);
        }

        return Ok();
    }

    [HttpDelete]
    [Authorize]
    [Route("/api/{type}/{id}")]
    public IActionResult DeleteComponent(string type, int id)
    {
        _logger.Information("Deleting component of type: {type} with ID: {id}", type, id);

        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
        {
            _logger.Warning("Unauthorized access attempt to delete component of type: {type} with ID: {id}", type, id);
            return Unauthorized();
        }

        if (_componentServices.Delete(user, id))
        {
            _logger.Information("Successfully deleted component of type: {type} with ID: {id} for user: {user}", type, id, user.Email);
            return Ok();
        }

        _logger.Warning("Failed to delete component of type: {type} with ID: {id}", type, id);
        return BadRequest();
    }

    [HttpPut]
    [Authorize]
    [Route("/api/{type}/{id}")]
    public IActionResult UpdateComponent(string type, int id, [FromBody] Dictionary<string, string> body)
    {
        _logger.Information("Updating component of type: {type} with ID: {id}", type, id);

        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
        {
            _logger.Warning("Unauthorized access attempt to update component of type: {type} with ID: {id}", type, id);
            return Unauthorized();
        }

        if (_componentServices.Update(user, body, id))
        {
            _logger.Information("Successfully updated component of type: {type} with ID: {id} for user: {user}", type, id, user.Email);
            return Ok();
        }

        _logger.Warning("Failed to update component of type: {type} with ID: {id}", type, id);
        return BadRequest();
    }
}