using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Entities;
using System.Security.Claims;

namespace Restaurants.Infrastructure.Authorization
{
    public class RestaurantsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
    {
        //We are creating this to add the attribute we added to the user class that extends the IdentityUser 
        //to the UserClaimsPrincipalFactory so that when a user is authenticated, the access token of the user
        //can contain the new attribute and hence we can access the claims values of that attribute from the authenticated
        //use access token
        public RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {

        }

        //We are creating this to add the attribute we added to the user class that extends the IdentityUser 
        //to the UserClaimsPrincipalFactory so that when a user is authenticated, the access token of the user
        //can contain the new attribute and hence we can access the claims values of that attribute from the authenticated
        //use access token
        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            //Creates a default claims and then our custom claims are added ontop of it
            var id = await GenerateClaimsAsync(user);

            if(user.Nationality != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.Nationality, user.Nationality));
            }

            if(user.DateOfBirth != null)
            {
                id.AddClaim(new Claim(AppClaimTypes.DateOfBirth, user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
            }

            return new ClaimsPrincipal(id);
        }
    }
}
