using Business.Entity;
using Business.Field.validators;
using Data.Models.components;

namespace Business.Field.factory;

public class ValidationFactory
{
    public IFieldValidator CreateValidator(ComponentField field)
    {
        AbstractFieldValidator validator = new DefaultValidator();

        foreach (ComponentValidation validation in field.Validations)
        {
            AbstractFieldValidator? result = validation.ValidationType.ToLower() switch
            {
                "required" => new RequiredValidator(validation),
                "length" => new LengthValidator(validation),
                "min-length" => new MinLengthValidator(validation),
                "max-length" => new MaxLengthValidator(validation),
                "min" => new MinValidator(validation),
                "max" => new MaxValidator(validation),
                _ => null
            };
            
            if (result != null)
            {
                validator.SetNext(result);
            }
        }

        return validator;
    }
}