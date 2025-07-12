
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants.Dto;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQuery : IRequest<PagedResult<RestaurantDto>>
    {
        public GetAllRestaurantsQuery(string searchPhrase, int pageNumber, int pageSize)
        {
            SearchPhrase = searchPhrase;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
        public string SearchPhrase { get; }
        public int PageNumber { get; }
        public int PageSize { get; }
        public string? SortBy { get; set; }
        public SortDirection SortDirection { get; set; }
    }
}
