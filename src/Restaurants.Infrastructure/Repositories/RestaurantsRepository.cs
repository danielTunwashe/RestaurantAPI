
using Microsoft.EntityFrameworkCore;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;
using Restaurants.Infrastructure.DataAccess;
using System.Linq.Expressions;

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

        public async Task<(IEnumerable<Restaurant>, int)> GetAllMatchingAsync(string? searchQuery, int pageSize, 
        int pageNumber, string? sortBy, SortDirection sortDirection)
        {
            var searchFilterToLower = searchQuery?.ToLower();

            var baseQuery = _dbContext.Restaurants
            
             //This ensures that if the search parameter is empty,
             //it returns all the result
            .Where(r => searchFilterToLower == null 
             || (r.Name.ToLower().Contains(searchFilterToLower)
             || r.Description.Contains(searchFilterToLower)));

            var totalCount = await baseQuery.CountAsync();

            //Want to apply sorting....
            if(sortBy != null)
            {
                //We want to create a Dictionary that have a value type matching the
                //OrderBy() Value
                var columSelector = new Dictionary<string, Expression<Func<Restaurant, object>>>()
                {
                    {nameof(Restaurant.Name), r => r.Name},
                    {nameof(Restaurant.Description), r => r.Description},
                    {nameof(Restaurant.Category), r => r.Category}
                };

                var selectedColumn = columSelector[sortBy];

                baseQuery = sortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
                    
            }

             var resturants = await baseQuery
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync();
            
            return (resturants, totalCount);

            //PageSize = 5, PageNumber = 3: Skip => PageSize * (PageNumber - 1) => 5 * (3 - 1) => 10
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
