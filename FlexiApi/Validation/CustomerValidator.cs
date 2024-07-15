using Data.Models;
using FluentValidation;

namespace FlexiApi.Validation;

public class CustomerValidator : FlexiValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(customer => customer.FirstName)
            .NotEmpty()
            .WithMessage("FirstName: First name cannot be empty!");
            
        RuleFor(customer => customer.Email)
            .NotEmpty()
            .WithMessage("Email: Email cannot be empty!");
        
        RuleFor(customer => customer.Phone)
            .Matches(@"^(\+[0-9]{10})$")
            .When(customer => !string.IsNullOrEmpty(customer.Phone))
            .WithMessage("Phone: Phone number is not valid!");
    }

    public override Type GetValidatorType()
    {
        return typeof(Customer);
    }
}