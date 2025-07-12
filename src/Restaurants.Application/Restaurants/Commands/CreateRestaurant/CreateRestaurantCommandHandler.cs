using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandHandler(IRestauantsRepository repository, ILogger<CreateRestaurantCommandHandler> logger, IMapper mapper, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<CreateRestaurantCommandHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;
        private readonly IUserContext _userContext = userContext;   

        public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var currentUser = _userContext.GetCurrentUser();

            
            _logger.LogInformation("{UserEmail} [{UserId}] is Creating a new restaurant...{@Restaurant}", request, currentUser.Email, currentUser.Id);

            //Map first for input b4 allowing user to type
            var newRestaurant = _mapper.Map<Restaurant>(request);
            newRestaurant.OwnerId = currentUser.Id;

            int id = await _repository.Create(newRestaurant);
            return id;
        }
    }
}
