using Auth.Attributes;
using Business;
using Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[Route("api/[controller]")]
public class InstanceController : Controller
{
    
    private readonly InstanceServices _instanceServices;

    public InstanceController(InstanceServices instanceServices)
    {
        _instanceServices = instanceServices;
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateInstance([FromBody] Instance instance)
    {
        if (_instanceServices.CreateInstance(instance))
            return Ok();

        return BadRequest();
    }
    
}