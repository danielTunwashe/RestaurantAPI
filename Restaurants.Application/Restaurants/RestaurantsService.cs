using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dto;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants
{
    public class RestaurantsService(IRestauantsRepository repository, ILogger<RestaurantsService> logger, IMapper mapper) : IRestaurantsService
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<RestaurantsService> _logger = logger;
        private readonly IMapper _mapper = mapper;
        public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
        {
            _logger.LogInformation("Getting all Restaurants...");
            var restaurants = await _repository.GetAllAsync();

            //This is for Manual Mapping
            //var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);


            //This is for AutoMapper
            var restaurantsDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantsDtos!;
        }

        public async Task<RestaurantDto?> GetRestaurantById(int id)
        {
            _logger.LogInformation("Getting Restaurant By Id");
            var restaurant = await _repository.GetByIdAsync(id);

            //Manual Mapping
            //var restaurantDto = RestaurantDto.FromEntity(restaurant);

            //Auto Mapper
            var restaurantDto = _mapper.Map<RestaurantDto?>(restaurant);

            return restaurantDto;
        }
    }
}
