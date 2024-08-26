using Auth;
using Auth.Attributes;
using Business;
using Business.Services;
using Data.Models;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FlexiApi.Controllers;

public class EntityController : FlexiController
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
        User? user = _authManager.GetLoggedInUser(HttpContext);
        if (user == null) return Unauthorized();

        JObject json = JObject.Parse(body);
        Instance instance = user.Organization.Instance;
        
        Result result = _entityServices.Import(json, instance);
        return HandleResult(result);
    }
}