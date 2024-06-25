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
    
    [HttpGet]
    public IActionResult GetInstance(string instanceKey, string email)
    {
        try
        {
            Instance instance = _instanceServices.GetInstance(instanceKey, email);
            return Ok(ApiResponse<Instance>.Success(instance));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
    
    [HttpPut]
    [Validation(typeof(Instance))]
    public IActionResult UpdateInstance([FromBody] Instance instance)
    {
        try
        {
            _instanceServices.UpdateInstance(instance);
            return Ok(ApiResponse<string>.Success("Instance updated successfully"));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
    
    [HttpPatch]
    public IActionResult RegenerateInstanceKey(string instanceKey)
    {
        try
        {
            string newKey = _instanceServices.RegenerateInstanceKey(instanceKey);
            return Ok(ApiResponse<string>.Success(newKey));
        }
        catch (Exception e)
        {
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
}