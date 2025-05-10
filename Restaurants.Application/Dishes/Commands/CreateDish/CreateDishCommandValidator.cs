
using FluentValidation;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandValidator : AbstractValidator<CreateDishCommand>
    {
        public CreateDishCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100).WithMessage("Name length must have a minimum value of 3 and max of 100");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description cannot be Empty");

            RuleFor(dto => dto.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price must be a non-negative number");

            RuleFor(dto => dto.KiloCalories)
                .GreaterThanOrEqualTo(0).WithMessage("KiloCalories must be a non-negative number");

        }
    }
}
