using System.Reflection;
using Data.Models;
using FlexiApi.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FlexiApi.Attributes;

public class ValidationActionFilter : IActionFilter
{
    private Dictionary<Type, IFlexiValidator> _validators = new Dictionary<Type, IFlexiValidator>();

    public ValidationActionFilter()
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        Type[] types = assembly.GetTypes();

        foreach (Type type in types)
        {
            if (typeof(IFlexiValidator).IsAssignableFrom(type) && !type.IsInterface && !type.IsAbstract)
            {
                IFlexiValidator validator = (IFlexiValidator)Activator.CreateInstance(type);
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
            if (!this._validators.ContainsKey(paramType)) continue;

            IFlexiValidator validator = this._validators[paramType];
            if (!validator.IsValid(param))
            {
                string[] errors = validator.GetErrors();
                context.Result = new BadRequestObjectResult(errors);

                break;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}