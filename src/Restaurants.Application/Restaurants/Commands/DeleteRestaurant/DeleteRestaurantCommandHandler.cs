using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Interfaces;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(IRestauantsRepository repository, ILogger<DeleteRestaurantCommandHandler> logger, IMapper mapper, IRestaurantAuthorizationService restaurantAuthorizationService) : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger = logger;
        private readonly IRestaurantAuthorizationService _restaurantAuthorizationService = restaurantAuthorizationService;
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting restuarants with id : {RestaurantId}", request.Id);
            var restaurant = await _repository.GetByIdAsync(request.Id);
            if(restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
            }

            if (!_restaurantAuthorizationService.Authorize(restaurant, ResourceOperation.Delete))
                throw new ForbiddenException();

            await _repository.Delete(restaurant);
        }


    }
}
