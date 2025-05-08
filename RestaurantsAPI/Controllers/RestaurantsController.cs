using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Dto;
using Restaurants.Domain.Entities;

namespace RestaurantsAPI.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantsService _restaurantsService;
        public RestaurantsController(IRestaurantsService restaurantsService)
        {
            _restaurantsService = restaurantsService;   
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allRestaurants = await _restaurantsService.GetAllRestaurants();
            return Ok(allRestaurants);
        }



        [HttpGet("GetRestaurantById/{id}")]
        public async Task<IActionResult> GetRestaurantById([FromRoute] int id)
        {
            var specificRestaurant = await _restaurantsService.GetRestaurantById(id);
            if (specificRestaurant == null)
            {
                return NotFound();
            }
            return Ok(specificRestaurant);
        }


        //[HttpPost("CreateNewRestaurant")]
        //public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto input)
        //{

        //}
    }
}
