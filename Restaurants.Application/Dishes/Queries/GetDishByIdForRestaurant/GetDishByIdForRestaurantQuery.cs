using MediatR;
using Restaurants.Application.Dishes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant
{
    public class GetDishByIdForRestaurantQuery : IRequest<DishDto>
    {
        public GetDishByIdForRestaurantQuery(int restaurantId, int dishId)
        {
            RestaurantId = restaurantId;
            DishId = dishId;
        }
        public int RestaurantId { get; }
        public int DishId { get; }
    }
}
