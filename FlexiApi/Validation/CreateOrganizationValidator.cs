using FlexiApi.InputModels;
using FlexiApi.Resources;
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
        Console.WriteLine(localizer["EmailRequired"].ResourceNotFound);
        Console.WriteLine(localizer["EmailRequired"].Name);
        Console.WriteLine(localizer["EmailRequired"].SearchedLocation);
        
        Console.WriteLine(localizer.GetAllStrings().Count());
    }
    
    public override Type GetValidatorType()
    {
        return typeof(CreateOrganization);
    }
}