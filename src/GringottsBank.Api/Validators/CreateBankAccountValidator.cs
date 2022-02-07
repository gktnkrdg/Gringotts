using FluentValidation;
using GringottsBank.Application.Models.Account;

namespace GringottsBank.Api.Validators
{
    public class CreateBankAccountValidator : AbstractValidator<CreateBankAccountCommand>
    {
        public CreateBankAccountValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("Bank account name cannot be empty.");
        }
    }
}