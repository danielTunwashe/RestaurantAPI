using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommandHandler(IRestauantsRepository repository, ILogger<DeleteRestaurantCommandHandler> logger, IMapper mapper) : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger = logger;
        public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting restuarants with id : {RestaurantId}", request.Id);
            var restaurant = await _repository.GetByIdAsync(request.Id);
            if(restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant),request.Id.ToString());
            }
            await _repository.Delete(restaurant);
        }


    }
}
