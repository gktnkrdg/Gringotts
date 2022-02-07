using FluentValidation;
using GringottsBank.Application.Models.Transaction;

namespace GringottsBank.Api.Validators
{
    public class WithdrawCommandValidator : AbstractValidator<WithdrawCommand>
    {
        public WithdrawCommandValidator()
        {
            RuleFor(p => p.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.");
        }
    }
}