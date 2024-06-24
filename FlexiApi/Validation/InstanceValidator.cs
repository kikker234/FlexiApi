using Data.Models;
using FluentValidation;

namespace FlexiApi.Validation;

public class InstanceValidator : FlexiValidator<Instance>
{
    public InstanceValidator()
    {
        RuleFor(instance => instance.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(instance => instance.Description)
            .NotEmpty()
            .WithMessage("Description is required");
    }

    public override Type GetValidatorType()
    {
        return typeof(Instance);
    }
}