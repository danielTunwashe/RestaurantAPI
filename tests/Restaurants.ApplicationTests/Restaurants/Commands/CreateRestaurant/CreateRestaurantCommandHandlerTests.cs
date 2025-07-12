using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Xunit;


namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            //arrange (This is where you prepare everything your test needs....)

            // Creating mocks for dependencies
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurant();
            mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);


            // Mocking the IMapper to return a Restaurant entity when mapping from CreateRestaurantCommand
            var restaurantRepositoryMock = new Mock<IRestauantsRepository>();
            // Mocking the Create method to return a restaurant ID of 1
            restaurantRepositoryMock.Setup(repo => repo.Create(It.IsAny<Restaurant>()))
                .ReturnsAsync(1); // Simulate returning a restaurant ID of 1


            // Mocking the IUserContext to return a current user
            var userContextMock = new Mock<IUserContext>();
            var currentUser = new CurrentUser("owner-id", "test@test.com", [], null, null);
            userContextMock.Setup(uc => uc.GetCurrentUser())
                .Returns(currentUser);


            var commandHandler = new CreateRestaurantCommandHandler(
                restaurantRepositoryMock.Object,
                loggerMock.Object,
                mapperMock.Object,
                userContextMock.Object
            );


            //act (This is where you call the method or api you want to test..)
            var result = await commandHandler.Handle(command, CancellationToken.None);


            //assert (This is where you check if the result is what you expected...)
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("owner-id");
            restaurantRepositoryMock.Verify(repo => repo.Create(restaurant), Times.Once);
        }
    }
}