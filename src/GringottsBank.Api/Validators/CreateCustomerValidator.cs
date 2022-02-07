using FluentValidation;
using GringottsBank.Application.Models.Customer;

namespace GringottsBank.Api.Validators
{

    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email address cannot be empty.")
                .EmailAddress().WithMessage("Please enter valid email address");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty.")
                .MinimumLength(6)
                .WithMessage("Password length must be minimum 6.");

            RuleFor(p => p.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("First name cannot be empty.");
            
            RuleFor(p => p.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Lastname cannot be empty");
         
           

        }
    }
}