
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dto;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(IRestauantsRepository repository, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<GetAllRestaurantsQueryHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all Restaurants...");
            var restaurants = await _repository.GetAllAsync();

            //This is for Manual Mapping
            //var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);


            //This is for AutoMapper
            var restaurantsDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantsDtos!;
        }
    }
}
