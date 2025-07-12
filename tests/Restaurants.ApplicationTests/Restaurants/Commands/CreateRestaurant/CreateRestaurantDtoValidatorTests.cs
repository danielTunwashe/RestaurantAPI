using FluentValidation.TestHelper;
using Xunit;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantDtoValidatorTests
    {
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Test",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12-345",
            };

            var validator = new CreateRestaurantDtoValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveAnyValidationError();
        }


        [Fact()]
        public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
        {
            //arrange
            var command = new CreateRestaurantCommand()
            {
                Name = "Te",
                Category = "Ita",
                PostalCode = "12345",
            };

            var validator = new CreateRestaurantDtoValidator();

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveValidationErrorFor( c=> c.Name);
            result.ShouldHaveValidationErrorFor( c=> c.Category);
            result.ShouldHaveValidationErrorFor( c=> c.PostalCode);
        }


        [Theory()]
        [InlineData("Italian")]
        [InlineData("Mexican")]
        [InlineData("Japanese")]
        [InlineData("American")]
        [InlineData("Indian")]
        public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
         {
            //arrange
            var validator = new CreateRestaurantDtoValidator();
            var command = new CreateRestaurantCommand { Category = category };

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldNotHaveValidationErrorFor(c => c.Category);
        }

        [Theory()]
        [InlineData("10220")]
        [InlineData("102-20")]
        [InlineData("10 220")]
        [InlineData("10-2 20")] 

        public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
        {
            //arrange
            var validator = new CreateRestaurantDtoValidator();
            var command = new CreateRestaurantCommand { PostalCode = postalCode };

            //act
            var result = validator.TestValidate(command);

            //assert
            result.ShouldHaveValidationErrorFor(c => c.PostalCode);
        }

    }
}