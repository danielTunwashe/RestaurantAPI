using FluentValidation;
using Restaurants.Application.Restaurants.Dto;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantDtoValidator : AbstractValidator<CreateRestaurantCommand>
    {
        //Adding a custom validator category to be from one of this
        private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "American", "Indian"];
        public CreateRestaurantDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 100);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required..");

            //Add a custom category to make sure that the value enterd is one of the values in the categories above
            RuleFor(dto => dto.Category)
                //.Custom((value, context) =>
                //{
                //    var isValidatorCategory = validCategories.Contains(value);
                //    if (!isValidatorCategory)
                //    {
                //        context.AddFailure("Category", "Invalid category please choose from the right category");
                //    }
                //});

                .Must(validCategories.Contains)
                .WithMessage("Invalid category please choose from the valid categories");

            //or

            RuleFor(dto => dto.ContactEmail)
                .NotEmpty().WithMessage("Please pprovide a valid Email Address");

            //RuleFor(dto => dto.ContactNumber)
            //    .Matches("^(?:\\+234|0)(7[0-9]|8[0-9]|9[0-9])[0-9]{7}$\r\n")
            //    .WithMessage("Please provide a valid phone number");

            RuleFor(dto => dto.PostalCode)
                .Matches("^\\d{6}$")
                .WithMessage("Please provide a valid postal code with six digit");


        }
    }
}
