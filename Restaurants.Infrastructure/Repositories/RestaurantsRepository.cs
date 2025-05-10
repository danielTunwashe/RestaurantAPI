
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.DataAccess;

namespace Restaurants.Infrastructure.Repositories
{
    public class RestaurantsRepository(RestaurantsDbContext dbcontext) : IRestauantsRepository
    {
        private readonly RestaurantsDbContext _dbContext = dbcontext;

        public async Task<int> Create(Restaurant input)
        {
           _dbContext.Restaurants.Add(input);
            await _dbContext.SaveChangesAsync();
            return input.Id;
        }

        public async Task Delete(Restaurant entity)
        {
            _dbContext.Restaurants.Remove(entity);  
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurant>> GetAllAsync()
        {
            var restaurants = await _dbContext.Restaurants.ToListAsync();
            return restaurants;
        }

        public async Task<Restaurant?> GetByIdAsync(int id)
        {
            var restuarant = await _dbContext.Restaurants.Include(d => d.Dishes).FirstOrDefaultAsync(r => r.Id == id);
            return restuarant;
        }

       
        public async Task UpdateAsync(Restaurant entity)
        {
            _dbContext.Restaurants.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
