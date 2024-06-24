using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace FlexiApi.Validation;

public abstract class FlexiValidator<out T> : AbstractValidator<T>
{

    private T t;
    
    public bool IsValid(T t)
    {
        if (t == null) this.t = t;
        
        return Validate(t).IsValid;
    }

    public string[]? GetErrors()
    {
        if (t == null) return null;
        
        ValidationResult result = Validate(t);
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