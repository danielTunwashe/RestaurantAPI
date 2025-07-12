
using Microsoft.AspNetCore.Identity;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.DataAccess;

namespace Restaurants.Infrastructure.Seeders
{
    public class RestaurantSeeder : IRestaurantSeeder
    {
        private readonly RestaurantsDbContext _dbContext;
        public RestaurantSeeder(RestaurantsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Restaurants.Any())
                {
                    var restaurant = GetRestaurants();
                    _dbContext.Restaurants.AddRange(restaurant);
                    await _dbContext.SaveChangesAsync();
                }

                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);   
                    await _dbContext.SaveChangesAsync();        
                }
            }
        }


        private IEnumerable<IdentityRole> GetRoles()
        {
            List<IdentityRole> roles =
            [
                new(UserRoles.User){
                    NormalizedName = UserRoles.User.ToUpper()
                },
                new(UserRoles.Owner){
                    NormalizedName = UserRoles.Owner.ToUpper()
                },
                new(UserRoles.Admin){
                    NormalizedName = UserRoles.Admin.ToUpper() 
                }
             ];

            return roles;   
        }


        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description ="KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Description="Some desc",
                            Price = 10.30M,
                            RestaurantId = 1
                        },

                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Description="Some desc 2",
                            Price = 5.30M,
                            RestaurantId = 2
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald Szewska",
                    Category = "Fast Food",
                    Description =  "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Szewska 2",
                        PostalCode = "30-001"
                    }
                }
            };

            return restaurants;
        }
    }
}
