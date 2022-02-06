using FluentValidation;
using GringottsBank.Application.Models.Customer;

namespace GringottsBank.Application.Validators
{

    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(p => p.Email)
                .NotNull()
                .NotEmpty()
                .WithMessage("Email adresi boş olamaz.")
                .EmailAddress().WithMessage("Geçerli email adresi giriniz");

            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Parola boş olamaz.")
                .MinimumLength(6)
                .WithMessage("Parola minimum 6 karakter olabilir.");

            RuleFor(p => p.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Ad bilgisi boş olamaz.");
            
            RuleFor(p => p.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Soyad bilgisi boş olamaz.");
         
           

        }
    }
}