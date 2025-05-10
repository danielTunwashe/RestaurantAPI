using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dto;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Entities;

namespace RestaurantsAPI.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantsController(IMediator mediator)
        {
           _mediator = mediator;     
        }



        [HttpGet("GetAllRestaurant")]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll()
        {
           // var allRestaurants = await _restaurantsService.GetAllRestaurants();
            var allRestaurants = await _mediator.Send(new GetAllRestaurantsQuery());
            return Ok(allRestaurants);
        }



        [HttpGet("GetRestaurantById/{id}")]
        public async Task<ActionResult<RestaurantDto?>> GetRestaurantById([FromRoute] int id)
        {
             var restaurant = await _mediator.Send(new GetRestaurantByIdQuery(id));
             return Ok(restaurant);
        }


        [HttpDelete("DeleteRestaurant/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute] int id)
        {
            await _mediator.Send(new DeleteRestaurantCommand(id));
            return NoContent();

        }



        [HttpPatch("UpdateRestaurant/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateRestaurant([FromRoute] int id, UpdateRestaurantCommand command)
        {
            command.Id = id;
            await _mediator.Send(command);

            return NoContent();
         
        }


        [HttpPost("CreateNewRestaurant")]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
           //int id = await _restaurantsService.CreateRestaurant(input);
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);

        }
    }
}
