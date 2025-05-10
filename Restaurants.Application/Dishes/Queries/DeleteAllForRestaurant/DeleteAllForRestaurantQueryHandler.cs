using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.DeleteAllForRestaurant
{
    public class DeleteAllForRestaurantQueryHandler : IRequestHandler<DeleteAllForRestaurantQuery>
    {
        private readonly ILogger<DeleteAllForRestaurantQueryHandler> _logger;
        private readonly IRestauantsRepository _restauantsRepository;
        private readonly IDishesRepository _dishesRepository;

        public DeleteAllForRestaurantQueryHandler(ILogger<DeleteAllForRestaurantQueryHandler> logger, IRestauantsRepository restauantsRepository, IDishesRepository dishesRepository)
        {
            _logger = logger;
            _restauantsRepository = restauantsRepository;
            _dishesRepository = dishesRepository;   
        }
        public async Task Handle(DeleteAllForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting all Dishes for restaurant based on id: {RestaurantId}", request.RestaurantId);
            var restaurant = await _restauantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            await _dishesRepository.DeleteAll(restaurant.Dishes);
        }
    }
}
