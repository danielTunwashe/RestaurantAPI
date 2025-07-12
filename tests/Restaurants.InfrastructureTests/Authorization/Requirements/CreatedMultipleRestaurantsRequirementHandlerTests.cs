using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using System.Threading.Tasks;
using Xunit;


namespace Restaurants.Infrastructure.Authorization.Requirements.Tests
{
    public class CreatedMultipleRestaurantsRequirementHandlerTests
    {
        [Fact()]
        public async Task HandleRequirementAsync_UserHasCreatedMultipleRestaurants_ShouldSuccees()
        {
            //arrange

            // Setting up a mock for the current user
            var currentUser = new CurrentUser("1","test@gmail.com", [], null, null);

            // Mocking the IUserContext to return the current user
            var userContextMock = new Mock<IUserContext>();

            // Using the Mock SetUp to return the current user
            userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            // Creating a list of restaurants where the current user has created multiple restaurants
            var restaurant = new List<Restaurant>
            {
                new Restaurant { OwnerId = currentUser.Id },
                new Restaurant { OwnerId = currentUser.Id },
                new Restaurant { OwnerId = "2" },
            };


            // Mocking the IRestauantsRepository to return the list of restaurants
            var restaurantRepositoryMock = new Mock<IRestauantsRepository>();
            restaurantRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(restaurant);

            // Creating the requirement for the handler to check if the user has created at least 2 restaurants
            var requirement = new CreatedMultipleRestaurantsRequirement(2);

            // Creating the handler with the mocked dependencies
            var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantRepositoryMock.Object, userContextMock.Object);

            // Creating the AuthorizationHandlerContext with the requirement
            var context = new AuthorizationHandlerContext([ requirement ], null, null);


            //act
            // Calling the HandleRequirementAsync method to check if the user meets the requirement
            await handler.HandleAsync(context);


            //assert
            //// Verifying that the user has succeeded in the requirement
            context.HasSucceeded.Should().BeTrue();

        }



        [Fact()]
        public async Task HandleRequirementAsync_UserHasNotCreatedMultipleRestaurants_ShouldFail()
        {
            //arrange
            // Setting up a mock for the current user
            var currentUser = new CurrentUser("1", "test@gmail.com", [], null, null);

            // Mocking the IUserContext to return the current user
            var userContextMock = new Mock<IUserContext>();

            // Using the Mock SetUp to return the current user
            userContextMock.Setup(m => m.GetCurrentUser()).Returns(currentUser);

            // Creating a list of restaurants where the current user has created multiple restaurants
            var restaurant = new List<Restaurant>
            {
                new Restaurant { OwnerId = currentUser.Id },
                new Restaurant { OwnerId = "2" },
            };


            // Mocking the IRestauantsRepository to return the list of restaurants
            var restaurantRepositoryMock = new Mock<IRestauantsRepository>();
            restaurantRepositoryMock.Setup(repo => repo.GetAllAsync())
                .ReturnsAsync(restaurant);

            // Creating the requirement for the handler to check if the user has created at least 2 restaurants
            var requirement = new CreatedMultipleRestaurantsRequirement(2);

            // Creating the handler with the mocked dependencies
            var handler = new CreatedMultipleRestaurantsRequirementHandler(restaurantRepositoryMock.Object, userContextMock.Object);

            // Creating the AuthorizationHandlerContext with the requirement
            var context = new AuthorizationHandlerContext([requirement], null, null);


            //act
            // Calling the HandleRequirementAsync method to check if the user meets the requirement
            await handler.HandleAsync(context);


            //assert
            //// Verifying that the user has succeeded in the requirement
            context.HasSucceeded.Should().BeFalse();
            context.HasFailed.Should().BeTrue();

        }

    }
}