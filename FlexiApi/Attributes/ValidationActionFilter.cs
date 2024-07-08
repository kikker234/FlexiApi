using System.Reflection;
using System.Text.RegularExpressions;
using Data.Models;
using FlexiApi.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlexiApi.Attributes;

public class ValidationActionFilter : IActionFilter
{
    private Dictionary<Type, IFlexiValidator> _validators = new();

    public ValidationActionFilter(IServiceProvider serviceProvider)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type[] types = assembly.GetTypes();

        foreach (Type type in types)
        {
            if (typeof(IFlexiValidator).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                // get validator from DI
                IFlexiValidator? validator = (IFlexiValidator?)serviceProvider.GetService(type);

                if (validator == null) continue;

                _validators.Add(validator.GetValidatorType(), validator);
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        object[] parameters = context.ActionArguments.Values.ToArray();

        foreach (object param in parameters)
        {
            Type paramType = param.GetType();
            if (!_validators.ContainsKey(paramType)) continue;

            IFlexiValidator validator = _validators[paramType];
            if (validator.IsValid(param)) continue;
            
            string[]? errors = validator.GetErrors();
            if(errors == null) continue;
            
            Dictionary<string, string> errorDictionary = new();
            foreach (string error in errors)
            {
                string[] errorParts = error.Split(":");
                
                if (errorParts.Length == 2)
                {
                    if(errorDictionary.ContainsKey(errorParts[0]))
                        errorDictionary[errorParts[0]] += $", {errorParts[1]}";
                    else
                        errorDictionary.Add(errorParts[0], errorParts[1]);
                    
                    Console.WriteLine("Added error for: " + errorParts[0]);
                }
            }
            
            context.Result = new BadRequestObjectResult(errorDictionary);
            break;
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}