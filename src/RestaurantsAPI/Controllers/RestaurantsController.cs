using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.DeleteRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Application.Restaurants.Dto;
using Restaurants.Application.Restaurants.Queries.GetAllRestaurants;
using Restaurants.Application.Restaurants.Queries.GetRestaurantById;
using Restaurants.Domain.Constants;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Authorization;

namespace RestaurantsAPI.Controllers
{
    [ApiController]
    [Route("api/restaurants")]
    [Authorize]
    public class RestaurantsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantsController(IMediator mediator)
        {
           _mediator = mediator;     
        }



        [HttpGet("GetAllRestaurant")]
        [AllowAnonymous]
        //[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantDto>))]
        public async Task<ActionResult<IEnumerable<RestaurantDto>>> GetAll([FromQuery] string? query, int pageNumber, int pageSize)
        {
            // var allRestaurants = await _restaurantsService.GetAllRestaurants();
            var allRestaurants = await _mediator.Send(new GetAllRestaurantsQuery(query,pageNumber,pageSize));
            return Ok(allRestaurants);
        }



        [HttpGet("GetRestaurantById/{id}")]
        [Authorize(Policy = PolicyNames.HasNationality)]
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
        //Only allow a user that has an owner role to access this endpoint
        [Authorize(Roles = UserRoles.Owner)]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
           //int id = await _restaurantsService.CreateRestaurant(input);
            int id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRestaurantById), new { id }, null);

        }
    }
}
