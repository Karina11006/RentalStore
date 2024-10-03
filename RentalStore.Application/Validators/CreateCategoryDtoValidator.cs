using FluentValidation;
using RentalStore.SharedKernel.Dto;

namespace RentalStore.Application.Validators
{
    public class CreateCategoryDtoValidator : AbstractValidator<CategoryDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name can't be longer than 100 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description can't be longer than 500 characters.");
        }
    }
}
