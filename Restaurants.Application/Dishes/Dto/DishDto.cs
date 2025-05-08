
using Restaurants.Domain.Entities;

namespace Restaurants.Application.Dishes.Dto
{
    public class DishDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int? KiloCalories { get; set; }
        public int RestaurantId { get; set; }


        //public static DishDto? FromEntity(Dish? dish)
        //{
        //    if(dish == null)
        //    {
        //        return null;
        //    }
        //    return new DishDto
        //    {
        //        Id = dish.Id,
        //        Name = dish.Name,
        //        Description = dish.Description,
        //        Price = dish.Price,
        //        KiloCalories = dish.KiloCalories,
        //        RestaurantId = dish.RestaurantId,
        //    };
        //}

    }
}
