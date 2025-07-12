using AutoMapper;
using FluentAssertions;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Restaurants.Application.Restaurants.Commands.UpdateRestaurant;
using Restaurants.Domain.Entities;
using Xunit;


namespace Restaurants.Application.Restaurants.Dto.Tests
{
    public class RestaurantsProfileTests
    {
        private IMapper _mapper;
        public RestaurantsProfileTests()
        {
            //This allows the configuration object to know about all 
            //The mapping we have created in our RestaurantsProfile by Automapper
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<RestaurantsProfile>();
            });

            //This helps create the mock of a mapper that we can use for testcases
            _mapper = configuration.CreateMapper();
        }


        [Fact()]
        public void CreateMap_ForRestaurantToRestaurantDto_MapsCorrectly()
        {
            //arrange
            //A way to create mapper within our test case

            var restaurant = new Restaurant()
            {
                Id = 1,
                Name = "Test restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "123456789",
                Address = new Address
                {
                    City = "Test City",
                    Street = "Test Street",
                    PostalCode = "12345"
                }
            };


            //act
            var restaurantDto = _mapper.Map<RestaurantDto>(restaurant);


            //assert
            restaurantDto.Should().NotBeNull(); 
            restaurantDto.Id.Should().Be(restaurant.Id);
            restaurantDto.Description.Should().Be(restaurant.Description);
            restaurantDto.Category.Should().Be(restaurant.Category);
            restaurantDto.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurantDto.City.Should().Be(restaurant.Address.City);
            restaurantDto.Street.Should().Be(restaurant.Address.Street);
            restaurantDto.PostalCode.Should().Be(restaurant.Address.PostalCode);

        }


        [Fact()]
        public void CreateMap_ForCreateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            //arrange
            //A way to create mapper within our test case
           

            var command = new CreateRestaurantCommand
            {
                Name = "Test Restaurant",
                Description = "Test Description",
                Category = "Test Category",
                HasDelivery = true,
                ContactEmail = "test@example.com",
                ContactNumber = "123456789",
                City = "Test City",
                Street = "Test Street",
                PostalCode = "12345"
            };

            


            //act
            var restaurant = _mapper.Map<Restaurant>(command);


            //assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(restaurant.Id);
            restaurant.Description.Should().Be(restaurant.Description);
            restaurant.Category.Should().Be(restaurant.Category);
            restaurant.HasDelivery.Should().Be(restaurant.HasDelivery);
            restaurant.Address.City.Should().Be(restaurant.Address.City);
            restaurant.Address.Street.Should().Be(restaurant.Address.Street);
            restaurant.Address.PostalCode.Should().Be(restaurant.Address.PostalCode);

        }


        [Fact()]
        public void CreateMap_ForUpdateRestaurantCommandToRestaurant_MapsCorrectly()
        {
            //arrange
            //A way to create mapper within our test case

            var command = new UpdateRestaurantCommand
            {
                Id = 1,
                Name = "Updated Restaurant",
                Description = "Updated Description",
                HasDelivery = false
            };

            //act
            var restaurant = _mapper.Map<Restaurant>(command);


            //assert
            restaurant.Should().NotBeNull();
            restaurant.Id.Should().Be(command.Id);
            restaurant.Name.Should().Be(command.Name);
            restaurant.Description.Should().Be(command.Description);
            restaurant.HasDelivery.Should().Be(restaurant.HasDelivery);
           
        }

    }
}