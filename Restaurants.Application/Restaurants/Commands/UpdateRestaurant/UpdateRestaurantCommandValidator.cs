using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
    {
        public UpdateRestaurantCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name cannot be empty");

            RuleFor(dto => dto.Name)
                .Length(3, 100).WithMessage("Name Must be atleast 3 characters and max of 100 characters!");

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description cannot be empty!");


        }
    }
}
