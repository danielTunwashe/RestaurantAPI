using AutoMapper;
using Castle.Core.Logging;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;
using System.Threading.Tasks;
using Xunit;


namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        //First of all we create the mock for the dependencies that we will be using in our
        //UpdateCommandHandler
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestauantsRepository> _restaurantRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationService> _restaurantAuthorizationServiceMock;

        //Create a dependency injection for our updateCommandHandler
        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            // Initialize the mocks for the dependencies
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _restaurantRepositoryMock = new Mock<IRestauantsRepository>();
            _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationService>();

            // Create an instance of the UpdateRestaurantCommandHandler with the mocked dependencies
            _handler = new UpdateRestaurantCommandHandler(
                _restaurantRepositoryMock.Object,
                _loggerMock.Object,
                _mapperMock.Object,
                _restaurantAuthorizationServiceMock.Object
            );
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateRestaurants()
        {
            //arrange
            var restaurantId = 1;
            var command = new UpdateRestaurantCommand
            {
                Id = restaurantId,
                Name = "Updated Restaurant",
                Description = "Updated Description",
                HasDelivery = true
            };

            var restaurant = new Domain.Entities.Restaurant
            {
                Id = restaurantId,
                Name = "Old Restaurant",
                Description = "Old Description",
                HasDelivery = false
            };

            _restaurantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(restaurantId))
                .ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock
                .Setup(m => m.Authorize(restaurant, ResourceOperation.Update))
                .Returns(true);



            //act
            // This is where we call the method we want to test
            await _handler.Handle(command, CancellationToken.None);




            //assert
            // Verify that the repository's GetByIdAsync method was called with the correct restaurant ID
            _restaurantRepositoryMock
                .Verify(repo => repo.UpdateAsync(restaurant), Times.Once);
            // Verify that the authorization service was called with the correct parameters
            _mapperMock
                .Verify(m => m.Map(command, restaurant), Times.Once);
        }

        [Fact()]
        public async Task Handle_WithNonExistentRestaurant_ShouldThrowNotFoundException()
        {
            //arrange
            var restaurantId = 2;
            var command = new UpdateRestaurantCommand
            {
                Id = restaurantId,
            };
            _restaurantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(restaurantId))
                .ReturnsAsync((Restaurant?)null);


            //act
            Func<Task> act = async () =>
            {
                await _handler.Handle(command, CancellationToken.None);
            };

            //assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurant with id: {restaurantId} doesn't exist");

        }

        [Fact()]
        public async Task Handle_WithUnauthorizedUser_ShouldThrowForbiddenException()
        {
            //arrange
            var restaurantId = 3;
            var command = new UpdateRestaurantCommand
            {
                Id = restaurantId,
            };
            var restaurant = new Domain.Entities.Restaurant
            {
                Id = restaurantId,
                Name = "Test Restaurant",
                Description = "Test Description",
                HasDelivery = false
            };
            _restaurantRepositoryMock
                .Setup(repo => repo.GetByIdAsync(restaurantId))
                .ReturnsAsync(restaurant);
            _restaurantAuthorizationServiceMock
                .Setup(m => m.Authorize(restaurant, ResourceOperation.Update))
                .Returns(false);
            
            //act
            Func<Task> act = async () =>
            {
                await _handler.Handle(command, CancellationToken.None);
            };


            //assert
            await act.Should().ThrowAsync<ForbiddenException>();
        }
    }
}
