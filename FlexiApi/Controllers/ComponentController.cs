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
    
    [HttpGet]
    [Authorize]
    [Route("/api/{type}")]
    public IActionResult GetComponents(string type)
    {
        return Ok();
    }
    
    [HttpPost]
    [Authorize]
    [Route("/api/{type}")]
    public IActionResult CreateComponent(string type, [FromBody] ComponentDto componentDto)
    {
        return Ok();
    }     
}