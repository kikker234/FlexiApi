using Auth.Attributes;
using Business.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FlexiApi.Controllers;

public class EntityController : Controller
{
    
    private readonly EntityServices _entityServices;
    
    public EntityController(EntityServices entityServices)
    {
        _entityServices = entityServices;
    }
    
    [HttpPost]
    [Route("/api/entity/import")]
    public IActionResult ImportEntity([FromBody] string body)
    {
        Console.WriteLine(body);
        
        // ToDo: switch back on
        // if (!Request.Headers.ContainsKey("instance"))
        // {
        //     return BadRequest("Please define an instance key!");
        // }
        
        // parse body to json
        JObject json = JObject.Parse(body);
   
        try
        {
            bool result = _entityServices.Import(json);

            if (result == false)
            {
                return BadRequest("Unable to import entity map");
            }
        }
        catch (Exception e)
        {
            return BadRequest("Something went wrong! " + e.Message);
        }
        
        return Ok();
    }
    
}