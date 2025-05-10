using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Infrastructure.Repositories
{
    public class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
    {
        private readonly RestaurantsDbContext _dbContext = dbContext;
        public async Task<int> Create(Dish entity)
        {
            await _dbContext.Dishes.AddAsync(entity); 
            await _dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteAll(IEnumerable<Dish> entity)
        {
            _dbContext.Dishes.RemoveRange(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
