using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dto;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;


namespace Restaurants.Application.Restaurants.Queries.GetRestaurantById
{
    public class GetRestaurantByIdQueryHandler(IRestauantsRepository repository, ILogger<GetRestaurantByIdQueryHandler> logger, IMapper mapper) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto>
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<GetRestaurantByIdQueryHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;
        public async Task<RestaurantDto> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting Restaurant By Id {RestaurantId}", request.Id);
            var restaurant = await _repository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Restaurant), request.Id.ToString()); 

            //Manual Mapping
            //var restaurantDto = RestaurantDto.FromEntity(restaurant);

            //Auto Mapper
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);

            return restaurantDto;
        }
    }
}
