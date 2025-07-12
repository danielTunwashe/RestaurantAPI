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

namespace Restaurants.Application.Dishes.Queries.GetDishesForRestaurant
{
    public class GetDishesForRestaurantQueryHandler : IRequestHandler<GetDishesForRestaurantQuery, IEnumerable<DishDto>>
    {
        private readonly ILogger<GetDishesForRestaurantQueryHandler> _logger;
        private readonly IRestauantsRepository _restauantsRepository;
        private readonly IDishesRepository _dishesRepository;
        private readonly IMapper _mapper;
        public GetDishesForRestaurantQueryHandler(ILogger<GetDishesForRestaurantQueryHandler> logger, IRestauantsRepository restauantsRepository, IDishesRepository dishesRepository, IMapper mapper)
        {
            _logger = logger;
            _restauantsRepository = restauantsRepository;   
            _dishesRepository = dishesRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<DishDto>> Handle(GetDishesForRestaurantQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all dishes that belongs to a particular restuaurant by {RestaurantId}", request.RestaurantId);
            //Check if the restaurant based on the Id exist
            var restaurant = await _restauantsRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

            var dishes = _mapper.Map<IEnumerable<DishDto>>(restaurant.Dishes);
            return dishes;
        }
    }
}
