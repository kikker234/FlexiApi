using Data.Models;
using FeatureFlags;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Controllers;

[Route("/api/Features")]
public class FeatureFlagController : Controller
{
    
    private readonly FeatureFlagManager _featureFlagManager;
    
    public FeatureFlagController(FeatureFlagManager featureFlagManager)
    {
        _featureFlagManager = featureFlagManager;
    }
    
    [HttpGet("{instanceId}")]
    public IActionResult HasCustomFeatures(int instanceId)
    {
        Instance instance = new Instance { Id = instanceId };
        return Ok(_featureFlagManager.HasCustomFeatures(instance));
    }

    [HttpPost]
    public IActionResult CreateFeature(int instanceId, string featureName)
    {
        return Ok(_featureFlagManager.CreateFeature(instanceId, featureName));
    }

    [HttpPatch]
    public IActionResult Enable(int featureFlagId)
    {
        return Ok(_featureFlagManager.Enable(featureFlagId));
    }
    
    
}