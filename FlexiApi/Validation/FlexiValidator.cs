using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Validation;

public abstract class FlexiValidator<T> : AbstractValidator<T>, IFlexiValidator
{

    private T? _t;
    
    public bool IsValid(T t)
    {
        this._t = t;
        
        return Validate(t).IsValid;
    }

    bool IFlexiValidator.IsValid(object t)
    {
        if (t is T typedObject)
            return IsValid(typedObject);
        
        return false;
    }

    public string[]? GetErrors()
    {
        if (_t == null) return null;
        
        ValidationResult result = Validate(_t);
        if (result.IsValid) return null;
        
        List<string> errors = new();
        foreach (ValidationFailure failure in result.Errors)
        {
            errors.Add(failure.ErrorMessage);
        }

        return errors.ToArray();
    }

    public abstract Type GetValidatorType();
}

public interface IFlexiValidator
{
    bool IsValid(object t);
    string[]? GetErrors();
    Type GetValidatorType();
}