
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Common;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dto;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants.Queries.GetAllRestaurants
{
    public class GetAllRestaurantsQueryHandler(IRestauantsRepository repository, ILogger<GetAllRestaurantsQueryHandler> logger, IMapper mapper) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantDto>>
    {
        private readonly IRestauantsRepository _repository = repository;
        private readonly ILogger<GetAllRestaurantsQueryHandler> _logger = logger;
        private readonly IMapper _mapper = mapper;
        public async Task<PagedResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Getting all Restaurants...");
            var (restaurants,totalCount) = await _repository.GetAllMatchingAsync(request.SearchPhrase, request.PageNumber, request.PageSize,
             request.SortBy, request.SortDirection);

            //This is for Manual Mapping
            //var restaurantsDto = restaurants.Select(RestaurantDto.FromEntity);


            //This is for AutoMapper
            var restaurantsDtos = _mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            //This is for including the paginated PagedResult class 
            //Which takes in a generic Type and based on how defined in
            //the database logic, we are returning a tupule

            var result = new PagedResult<RestaurantDto>(restaurantsDtos, totalCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
