using System.Reflection;
using Data.Models;
using FlexiApi.Validation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlexiApi.Attributes;

public class ValidationActionFilter : IActionFilter
{
    
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var action = context.ActionDescriptor;
        var methodInfo = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)action).MethodInfo;
        var attribute = methodInfo.GetCustomAttributes(typeof(ValidationAttribute), false).FirstOrDefault() as ValidationAttribute;

        if (attribute == null) return;
        
        var type = attribute.Type;
        var validator = GetValidator<object>(type);

        if (validator == null)
        {
            // warning in the console
            System.Console.WriteLine($"Validator for {type.Name} not found");
            context.Result = new Microsoft.AspNetCore.Mvc.BadRequestObjectResult("Validator not found");
            return;
        }
        
        var model = context.ActionArguments.Values.FirstOrDefault();
        
        if (model == null) return;
        
        var result = validator.Validate(model);
        
        if (result.IsValid) return;
        
        context.Result = new Microsoft.AspNetCore.Mvc.BadRequestObjectResult(result);
    }
    
    private FlexiValidator<Instance>? GetValidator<T>(Type type)
    {
        Console.WriteLine(type);
        
        Type[] types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsSubclassOf(typeof(FlexiValidator<Instance>)))
            .ToArray();

        // log all types
        foreach (Type t in types)
        {
            System.Console.WriteLine(t.Name);
        }
        
        foreach (Type t in types)
        {
            if (typeof(FlexiValidator<T>).IsAssignableFrom(t))
            {
                FlexiValidator<Instance>? validator = (FlexiValidator<Instance>) Activator.CreateInstance(t);
                if (validator != null && validator.GetValidatorType() == type) return validator;
            }
        }

        return null;
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}