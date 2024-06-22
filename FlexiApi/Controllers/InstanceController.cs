using Auth.Attributes;
using Business;
using Data.Models;
using FlexiApi.Utils;
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
    public IActionResult CreateInstance([FromBody] Instance instance)
    {
        try
        {
            string jwt = _instanceServices.CreateInstance(instance);
            return Ok(ApiResponse<String>.Success(jwt));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<String>.Error(e));
        }
    }
}