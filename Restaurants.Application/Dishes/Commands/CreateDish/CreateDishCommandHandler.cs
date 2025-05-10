using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Dishes.Commands.CreateDish
{
    public class CreateDishCommandHandler : IRequestHandler<CreateDishCommand, int>
    {
        private readonly IRestauantsRepository _restaurantRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateDishCommandHandler> _logger;
        private readonly IDishesRepository _dishesRepository;
        public CreateDishCommandHandler(IRestauantsRepository restaurantRepository, IMapper mapper, ILogger<CreateDishCommandHandler> logger, IDishesRepository dishesRepository)
        {
            _restaurantRepository = restaurantRepository;
            _mapper = mapper;
            _logger = logger;  
            _dishesRepository = dishesRepository;
        }
        public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creatting a new Dish {@DishRequest}", request);
            var restaurant = _restaurantRepository.GetByIdAsync(request.RestaurantId);
            if (restaurant == null) throw new NotFoundException(nameof(Restaurant),request.RestaurantId.ToString());

            //We have to do this because we will pass in an entity of type dish to the abstraction interface in the Domain layer
            //For it to be properly matched in the infrastructure layer
            var mappedDish = _mapper.Map<Dish>(request);

            return await _dishesRepository.Create(mappedDish);
        }
    }
}
