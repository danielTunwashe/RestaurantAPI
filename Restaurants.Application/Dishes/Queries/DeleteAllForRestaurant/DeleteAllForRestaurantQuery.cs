using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Dishes.Queries.DeleteAllForRestaurant
{
    public class DeleteAllForRestaurantQuery : IRequest
    {
        public DeleteAllForRestaurantQuery(int restaurantId)
        {
            RestaurantId = restaurantId;
        }

        public int RestaurantId { get; }
    }
}
