using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Interfaces;

namespace Restaurants.Infrastructure.Authorization.Services
{
    public class RestaurantAuthorizationService : IRestaurantAuthorizationService
    {
        private readonly IUserContext _userContext;
        private readonly ILogger<RestaurantAuthorizationService> _logger;
        public RestaurantAuthorizationService(IUserContext userContext, ILogger<RestaurantAuthorizationService> logger)
        {
            _logger = logger;
            _userContext = userContext;
        }
        public bool Authorize(Restaurant restaurant, ResourceOperation resourceOperation)
        {
            var currentUser = _userContext.GetCurrentUser();
            _logger.LogInformation("Authorizing user {UserEmail}, to {Operation} for restaurant {RestaurantName}", currentUser.Email, resourceOperation, restaurant);

            if (resourceOperation == ResourceOperation.Read || resourceOperation == ResourceOperation.Create)
            {
                _logger.LogInformation("Create/Read operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete && currentUser.IsInRole(UserRoles.Admin))
            {
                _logger.LogInformation("Admin User, delete operation - successful authorization");
                return true;
            }

            if (resourceOperation == ResourceOperation.Delete || resourceOperation == ResourceOperation.Update && currentUser.Id == restaurant.OwnerId)
            {
                _logger.LogInformation("Restaurant Owner - successful authorization");
                return true;
            }

            return false;
        }
    }
}
