using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Dto;
using Restaurants.Application.Dishes.Queries.DeleteAllForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Queries.GetDishesForRestaurant;

namespace RestaurantsAPI.Controllers
{
    [Route("api/restaurant/{restaurantId}/dishes")]
    [ApiController]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost("CreateNewDish")]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId;
            var dishId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetByIdForForRestaurant), new { restaurantId, dishId }, null);
        }

        [HttpGet("GetAllDishesForAParticularRestaurant")]
        public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await _mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
            return Ok(dishes);
        }

        [HttpGet("GetAParticularDishForARestaurantById/{dishId}")]
        public async Task<ActionResult<DishDto>> GetByIdForForRestaurant([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var dish = await _mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
            return Ok(dish);
        }

        [HttpDelete("DeleteAllDishesForARestaurantById")]
        public async Task<IActionResult> DeleteAllForRestaurant([FromRoute] int restaurantId)
        {
            await _mediator.Send(new DeleteAllForRestaurantQuery(restaurantId));
            return NoContent();
        }
    }
}
