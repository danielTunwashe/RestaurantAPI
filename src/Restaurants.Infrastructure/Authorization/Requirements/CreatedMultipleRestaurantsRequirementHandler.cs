
using Microsoft.AspNetCore.Authorization;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class CreatedMultipleRestaurantsRequirementHandler : AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
    {
        private readonly IRestauantsRepository _restauantsRepository;
        private readonly IUserContext _userContext;
        public CreatedMultipleRestaurantsRequirementHandler(IRestauantsRepository restauantsRepository, IUserContext userContext)
        {
            _restauantsRepository  = restauantsRepository;
            _userContext = userContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
        {
            var currentUser = _userContext.GetCurrentUser();

            var restaurants = await _restauantsRepository.GetAllAsync();

            var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser.Id);

            if(userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }
    }
}
