using Data.Models.components;

namespace Business.Field.validators;

public class MinValidator : AbstractFieldValidator
{
    public MinValidator(ComponentValidation validator) : base(validator)
    {
    }
    
    public override bool ValidateField(string value)
    {
        int length = 0;
        try
        {
            length = Convert.ToInt32(validator.ValidationValue);
        } catch (FormatException e)
        {
            Console.WriteLine("Validation value is not a number");
            return false;
        }
        
        if(value.Length <= length)
        {
            return false;
        }
        
        return base.ValidateField(value);
    }
}