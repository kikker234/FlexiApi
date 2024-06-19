using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FeatureFlags;

public class FeatureActionFilter : IActionFilter
{
    private readonly FeatureFlagManager _featureFlagManager;
    
    public FeatureActionFilter(FeatureFlagManager featureFlagManager)
    {
        _featureFlagManager = featureFlagManager;
    }
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.ActionDescriptor;
        var methodInfo = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)action).MethodInfo;
        var featureAttribute = methodInfo.GetCustomAttributes(typeof(FeatureAttribute), false).FirstOrDefault() as FeatureAttribute;

        if (featureAttribute != null)
        {
            // get instanceId from the JWT token
            int instanceId = -1;
            
            bool isFeatureEnabled = _featureFlagManager.HasFeature(featureAttribute.FeatureName, instanceId);
            if (!isFeatureEnabled)
                context.Result = new NotFoundResult();
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}