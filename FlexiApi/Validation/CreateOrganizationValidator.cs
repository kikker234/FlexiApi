using System.Globalization;
using FlexiApi.InputModels;
using FlexiApi.Resources;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FlexiApi.Validation;

public class CreateOrganizationValidator : FlexiValidator<CreateOrganization>
{

    public CreateOrganizationValidator(IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(dto => dto.OrganizationName)
            .NotEmpty().WithMessage("organizationName: Organization naam mag niet leeg zijn!")
            .MaximumLength(50).WithMessage("organizationName: Organization naam mag niet langer zijn dan 50 karakters!");
        
        RuleFor(dto => dto.Email)
            .EmailAddress()
            .WithMessage("email: Email is niet geldig!")
            .NotEmpty()
            .WithMessage("email: Email mag niet leeg zijn!")
            .MaximumLength(50).WithMessage("email: Email mag niet langer zijn dan 50 karakters!");

        RuleFor(dto => dto.Password)
            .NotEmpty()
            .MinimumLength(8).WithMessage("password: Wachtwoord moet minimaal 8 karakters lang zijn!");
        
        RuleFor(dto => dto.RepeatPassword)
            .Equal(dto => dto.Password)
            .WithMessage("repeatPassword: Wachtwoorden komen niet overeen!");
    }
    
    public override Type GetValidatorType()
    {
        return typeof(CreateOrganization);
    }
}