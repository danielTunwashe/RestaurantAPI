
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant
{
    public class UpdateRestaurantCommandHandler(IRestauantsRepository repository, ILogger<DeleteRestaurantCommandHandler> logger, IMapper mapper) : IRequestHandler<UpdateRestaurantCommand>
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;
        public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating Restaurants with id : {RestaurantId} with {@UpdatedRestaurant}", request.Id, request);
            var restaurant = await _repository.GetByIdAsync( request.Id );
            if( restaurant is null)
            {
                throw new NotFoundException(nameof(Restaurant), request.Id.ToString());
            }

            _mapper.Map(request, restaurant);

            //restaurant.Name = request.Name;
            //restaurant.Description = request.Description;
            //restaurant.HasDelivery = request.HasDelivery;

            await _repository.UpdateAsync(restaurant);
        }
    }
}
