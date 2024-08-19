using Auth;
using Auth.Attributes;
using Business;
using Business.Services;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FlexiApi.Controllers;

public class EntityController : Controller
{
    private readonly EntityServices _entityServices;
    private readonly InstanceServices _instanceServices;
    private readonly IAuthManager _authManager;

    public EntityController(IAuthManager authManager, EntityServices entityServices, InstanceServices instanceServices)
    {
        _entityServices = entityServices;
        _instanceServices = instanceServices;
        _authManager = authManager;
    }

    [HttpPost]
    [Authorize]
    [Route("/api/entity/import")]
    public IActionResult ImportEntity(string body)
    {
        JObject json = JObject.Parse(body);

        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null) return Unauthorized();

        Instance instance = user.Organization.Instance;

        try
        {
            bool result = _entityServices.Import(json, instance);

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