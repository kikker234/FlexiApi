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
public class TestController : Controller
{
    
    [HttpGet]
    public IActionResult Test()
    {
        return Ok("Test");
    }
    
}