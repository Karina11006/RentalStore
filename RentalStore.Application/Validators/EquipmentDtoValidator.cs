using FluentValidation;
using RentalStore.Application.Services;
using RentalStore.Domain.Interfaces;
using RentalStore.SharedKernel.Dto;


public class EquipmentDtoValidator : AbstractValidator<EquipmentDto>
{

    public EquipmentDtoValidator()
    {

        RuleFor(x => x.EquipmentId)
            .GreaterThan(0).WithMessage("Equipment ID must be greater than 0.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name can't be longer than 100 characters.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Category ID must be greater than 0.");

        RuleFor(x => x.Brand)
            .NotEmpty().WithMessage("Brand is required.")
            .MaximumLength(50).WithMessage("Brand can't be longer than 50 characters.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required.")
            .MaximumLength(50).WithMessage("Model can't be longer than 50 characters.");

        RuleFor(x => x.Condition)
            .Must(condition => condition == Condition.New || condition == Condition.Used)
            .WithMessage("Condition must be either 'New' or 'Used'.");

        
    }

        
}

