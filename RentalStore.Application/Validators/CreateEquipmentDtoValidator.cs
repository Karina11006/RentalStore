using FluentValidation;
using RentalStore.Domain.Interfaces;
using RentalStore.SharedKernel.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace RentalStore.Application.Validators
{
    public class CreateEquipmentDtoValidator : AbstractValidator<EquipmentDto>
    {
        public CreateEquipmentDtoValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(2).WithMessage("Name must be at least 2 characters long.")
                .MaximumLength(20).WithMessage("Name cannot exceed 20 characters.");

            RuleFor(e => e.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MinimumLength(2).WithMessage("Description must be at least 2 characters long.")
                .MaximumLength(200).WithMessage("Description cannot exceed 200 characters.");

            RuleFor(e => e.QuantityInStock)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity in stock must be greater than or equal to 0.");

            RuleFor(e => e.PricePerDay)
                .GreaterThanOrEqualTo(0).WithMessage("Price per day must be greater than or equal to 0.");

            RuleFor(e => e.CategoryName)
                .NotEmpty().WithMessage("Category name is required.");

            RuleFor(e => e.Brand)
                .NotEmpty().WithMessage("Brand is required.");

            RuleFor(e => e.Model)
                .NotEmpty().WithMessage("Model is required.");
        }
    }
}
