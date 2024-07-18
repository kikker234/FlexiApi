using Data.Models.components;
using FluentValidation;

namespace FlexiApi.Validation;

public class ComponentValidator : FlexiValidator<Component>
{

    public ComponentValidator()
    {
        RuleFor(comp => comp.Name)
            .NotEmpty();
    }
    
    public override Type GetValidatorType()
    {
        return typeof(Component);
    }
}