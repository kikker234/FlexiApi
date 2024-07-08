using FlexiApi.InputModels;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FlexiApi.Validation;

public class CreateOrganizationValidator : FlexiValidator<CreateOrganization>
{

    public CreateOrganizationValidator(IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(dto => dto.Email)
            .EmailAddress()
            .NotEmpty()
            .WithMessage(localizer["EmailRequired"]);
        
        RuleFor(dto => dto.Password)
            .NotEmpty()
            .WithMessage(localizer["PasswordRequired"]);
        
        Console.WriteLine(localizer["EmailRequired"].Value);
    }
    
    public override Type GetValidatorType()
    {
        return typeof(CreateOrganization);
    }
    
    public class ValidationMessages
    {
    }
}