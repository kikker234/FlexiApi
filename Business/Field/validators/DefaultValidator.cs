using Data.Models.components;

namespace Business.Field.validators;

public class DefaultValidator : AbstractFieldValidator
{
    
    public DefaultValidator() : base(null)
    {
    }
    
    public bool ValidateField(string value)
    {
        return base.ValidateField(value);
    }
}