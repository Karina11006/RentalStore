using FluentValidation;
using RentalStore.SharedKernel.Dto;

namespace RentalStore.Application.Validators
{
    public class RegisterCreateRentalDtoValidator : AbstractValidator<CreateRentalDto>
    {
        public RegisterCreateRentalDtoValidator()
        {
            RuleFor(x => x.RentalDate)
                .NotEmpty().WithMessage("Rental date is required.");

            RuleFor(x => x.ReturnDate)
                .GreaterThan(x => x.RentalDate).WithMessage("Return date must be later than the rental date.")
                .NotEmpty().WithMessage("Return date is required.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid status value.");

            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required.")
                .MaximumLength(50).WithMessage("Customer name must not exceed 50 characters.")
                .MinimumLength(2).WithMessage("Customer name must exceed 2 characters.");

            RuleFor(x => x.CustomerSurname)
                .NotEmpty().WithMessage("Customer surname is required.")
                .MaximumLength(50).WithMessage("Customer surname must not exceed 50 characters.")
                .MinimumLength(2).WithMessage("Customer surname must exceed 2 characters.");

            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage("Customer email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.CustomerPhone)
                .NotEmpty().WithMessage("Customer phone is required.")
                .Matches(@"^(?:\+\d{1,3}[- ]?)?\d{9}$").WithMessage("Invalid phone number format.");

            RuleFor(x => x.Total)
                .GreaterThanOrEqualTo(0).WithMessage("Total amount must be greater than or equal to 0.");
        }
    }
}
