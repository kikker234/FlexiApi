using FlexiApi.Validation;

namespace FlexiApi.Attributes;

public class ValidationAttribute : Attribute
{
    
    public Type Type { get; }
    
    public ValidationAttribute(Type Type)
    {
        this.Type = Type;
    }
    
}