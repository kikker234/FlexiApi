using Business.Entity.validators;
using Data.Models.components;

namespace Business.Field.validators;

public class RequiredValidator : AbstractFieldValidator
{
    public RequiredValidator(ComponentValidation validation) : base(validation)
    {
    }
    
    public override bool ValidateField(string value)
    {
        bool isRequired = Convert.ToBoolean(validator.ValidationValue);
        
        if (isRequired && string.IsNullOrEmpty(value))
        {
            return false;
        }

        return base.ValidateField(value);
    }
}