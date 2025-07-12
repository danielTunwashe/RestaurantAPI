using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastructure.Authorization.Requirements
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;
        private readonly IUserContext _userContext;
        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger, IUserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var currentUser = _userContext.GetCurrentUser();
            _logger.LogInformation("User: {Email}, date of birth {DOB} - Handling MinimumAgeRequirement", currentUser.Email, currentUser.DateOfBirth);

            if(currentUser.DateOfBirth == null)
            {
                _logger.LogWarning("User date of birth is null");
                context.Fail(); //To tell asp.net core framework that our requirement Failed 
                return Task.CompletedTask;
            }

            if(currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
            {
                _logger.LogInformation("Authorization succeeded");
                context.Succeed(requirement); //To tell asp.net core framework that our requirement is succeeded 
            }
            else
            {
                context.Fail(); //If the user age requirement does not match our requirement
            }

            return Task.CompletedTask;
        }
    }
}
