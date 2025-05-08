using Restaurants.Application.Dishes.Dto;
using Restaurants.Domain.Entities;


namespace Restaurants.Application.Restaurants.Dto
{
    public class RestaurantDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public bool HasDelivery { get; set; }

        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
        public List<DishDto?> Dishes { get; set; } = [];


        //public static RestaurantDto? FromEntity(Restaurant? restaurant)
        //{
        //    if(restaurant == null)
        //    {
        //        return null;
        //    }
        //    return new RestaurantDto()
        //    {
        //        Category = restaurant.Category,
        //        Name = restaurant.Name,
        //        Description = restaurant.Description,
        //        Id = restaurant.Id,
        //        City = restaurant.Address?.City,
        //        Street = restaurant.Address?.Street,
        //        PostalCode = restaurant.Address?.PostalCode,
        //        HasDelivery = restaurant.HasDelivery,
        //        Dishes = restaurant.Dishes.Select(DishDto.FromEntity).ToList()
        //    };
        //}
    }
}
