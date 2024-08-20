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
    private readonly Serilog.ILogger _logger;

    public InstanceController(InstanceServices instanceServices, Serilog.ILogger logger)
    {
        _instanceServices = instanceServices;
        _logger = logger;
    }

    [HttpPost]
    [Validation(typeof(Instance))]
    public IActionResult CreateInstance([FromBody] Instance instance)
    {
        _logger.Information("Creating new instance with the name {instance} from owner {email}", instance.Name, instance.OwnerEmail);
        
        try
        {
            string instanceKey = _instanceServices.CreateInstance(instance);
            
            _logger.Information("Instance created successfully with key: {key}", instanceKey);
            return Ok(ApiResponse<string>.Success(instanceKey));
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to create instance, with message: {message}", e.Message);
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
    
    [HttpGet]
    public IActionResult GetInstance(string instanceKey, string email)
    {
        _logger.Information("Getting instance with key: {key} and email: {email}", instanceKey, email);
        
        try
        {
            Instance? instance = _instanceServices.GetInstance(instanceKey, email);
            
            _logger.Information("Instance retrieved successfully with key: {key} and email: {email}", instanceKey, email);
            return Ok(ApiResponse<Instance>.Success(instance));
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to retrieve instance with key: {key} and email: {email}, with message: {message}", instanceKey, email, e.Message);
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
    
    [HttpPut]
    [Validation(typeof(Instance))]
    public IActionResult UpdateInstance([FromBody] Instance instance)
    {
        _logger.Information("Updating instance with key: {key}", instance.Key);
        
        try
        {
            if (!_instanceServices.UpdateInstance(instance))
            {
                _logger.Warning("Failed to update instance with key: {key}", instance.Key);
                return BadRequest(ApiResponse<string>.Error("Failed to update instance"));
            }
            
            _logger.Information("Instance updated successfully with key: {key}", instance.Key);
            return Ok(ApiResponse<string>.Success("Instance updated successfully"));
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to update instance with key: {key}, with message: {message}", instance.Key, e.Message);
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
    
    [HttpPatch]
    public IActionResult RegenerateInstanceKey(string instanceKey)
    {
        _logger.Information("Regenerating instance key for instance with key: {key}", instanceKey);
        
        try
        {
            string newKey = _instanceServices.RegenerateInstanceKey(instanceKey);
            
            _logger.Information("Instance key regenerated successfully for instance with key: {key}", instanceKey);
            return Ok(ApiResponse<string>.Success(newKey));
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to regenerate instance key for instance with key: {key}, with message: {message}", instanceKey, e.Message);
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
    
    [HttpDelete]
    public IActionResult DeleteInstance(string instanceKey)
    {
        _logger.Information("Deleting instance with key: {key}", instanceKey);
        
        try
        {
            Instance instance = _instanceServices.GetInstance(instanceKey);

            if (!_instanceServices.DeleteInstance(instance))
                throw new Exception("Failed to delete instance");    
            
            _logger.Information("Instance deleted successfully with key: {key}", instanceKey);
            return Ok(ApiResponse<string>.Success("Instance deleted successfully"));
        }
        catch (Exception e)
        {
            _logger.Error(e, "Failed to delete instance with key: {key}, with message: {message}", instanceKey, e.Message);
            return BadRequest(ApiResponse<string>.Error(e));
        }
    }
}