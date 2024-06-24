using Auth.Attributes;
using Business;
using Data.Models;
using FlexiApi.Attributes;
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
    // [Authorize]
    [Validation(typeof(Instance))]
    public IActionResult CreateInstance([FromBody] Instance instance)
    {
        try
        {
            string instanceKey = _instanceServices.CreateInstance(instance);
            return Ok(ApiResponse<string>.Success(instanceKey));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
}