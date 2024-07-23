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

    public ComponentController(IAuthManager authManager, ComponentServices componentServices)
    {
        _componentServices = componentServices;
        _authManager = authManager;
    }

    [HttpGet]
    [Authorize]
    [Route("/api/{type}")]
    public IActionResult GetComponentsAsync(string type)
    {
        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
            return Unauthorized();

        Instance instance = user.Organization.Instance;
        IEnumerable components = _componentServices.GetComponents(type, instance);

        return Ok(components);
    }

    [HttpGet]
    [Authorize]
    [Route("/api/{type}/{id}")]
    public IActionResult GetComponent(string type, int id)
    {
        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
            return Unauthorized();

        ComponentObject? component = _componentServices.GetComponent(id);
        if (component == null)
            return NotFound();

        return Ok(component);
    }

    [HttpPost]
    [Authorize]
    [Route("/api/{type}")]
    public IActionResult CreateComponent(string type, [FromBody] Dictionary<string, string> body)
    {
        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
            return Unauthorized();

        Instance instance = user.Organization.Instance;

        try
        {
            _componentServices.Create(user, instance, type, body);
        }
        catch (ComponentNotFoundException e)
        {
            return NotFound(e.Message);
        }
        catch (InvalidFieldDataException e)
        {
            return BadRequest(e.Message);
        }

        return Ok();
    }

    [HttpDelete]
    [Authorize]
    [Route("/api/{type}/{id}")]
    public IActionResult DeleteComponent(string type, int id)
    {
        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
            return Unauthorized();

        if (_componentServices.Delete(user, id))
            return Ok();

        return BadRequest();
    }

    [HttpPut]
    [Authorize]
    [Route("/api/{type}/{id}")]
    public IActionResult UpdateComponent(string type, int id, [FromBody] Dictionary<string, string> body)
    {
        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null)
            return Unauthorized();

        if (_componentServices.Update(user, body, id))
            return Ok();

        return BadRequest();
    }
}