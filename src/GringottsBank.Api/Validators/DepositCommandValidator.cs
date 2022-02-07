using FluentValidation;
using GringottsBank.Application.Models.Transaction;

namespace GringottsBank.Api.Validators
{
    public class DepositCommandValidator : AbstractValidator<DepositCommand>
    {
        public DepositCommandValidator()
        {
            RuleFor(p => p.Amount)
                .GreaterThan(0)
                .WithMessage("Amount must be greater than zero.");
        }
    }
}