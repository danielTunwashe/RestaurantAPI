using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(IRestauantsRepository repository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper) : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;
        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating a new restaurant...{@Restaurant}", request);

            //Map first for input b4 allowing user to type
            var newRestaurant = _mapper.Map<Restaurant>(request);

            int id = await _repository.Create(newRestaurant);
            return id;
        }
    }
}
