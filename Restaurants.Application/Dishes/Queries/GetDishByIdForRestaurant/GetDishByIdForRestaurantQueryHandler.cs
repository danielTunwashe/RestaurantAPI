using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Dishes.Dto;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQueryHandler : IRequestHandler<GetDishByIdForRestaurantQuery, DishDto>
    {
        private readonly ILogger<GetDishByIdForRestaurantQueryHandler> _logger;
        private readonly IRestauantsRepository _restauantsRepository;
        private readonly IMapper _mapper;
        public GetDishByIdForRestaurantQueryHandler(ILogger<GetDishByIdForRestaurantQueryHandler> logger, IRestauantsRepository restauantsRepository, IMapper mapper)
        {
            _logger = logger;   
            _restauantsRepository = restauantsRepository;
            _mapper = mapper;   
        }
        public async Task<DishDto> Handle(GetDishByIdForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting a particular dish for a restaurant based on {RestaurantId} and {DishId}", request.RestaurantId, request.DishId);
            var restaurant = await _restauantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dish = restaurant.Dishes.FirstOrDefault(d => d.Id == request.DishId);
            if(dish ==null) throw new NotFoundException(nameof(Dish), request.DishId.ToString());

            var mappedDish = _mapper.Map<DishDto>(dish);

            return mappedDish;

        }



    }
}
