using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Domain.Repositories
{
    public interface IRestauantsRepository
    {
        Task<IEnumerable<Restaurant>> GetAllAsync();
        Task<(IEnumerable<Restaurant>,int)> GetAllMatchingAsync(string? searchQuery, int pageSize, int pageNumber, string? sortBy, SortDirection sortDirection);
        Task<Restaurant?> GetByIdAsync(int id);
        Task<int> Create(Restaurant input);
        Task Delete(Restaurant entity);
        Task UpdateAsync(Restaurant entity);
    }
}
