using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Restaurants.Application.Extensions;
using Restaurants.Application.Middlewares;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using RestaurantsAPI.Extensions;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.AddPresentation();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();

//For the seedeer
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.seed();

//Create a custom error habdling middle ware services to track things that happens during request processing
//This is the actual usage of the middleware
app.UseMiddleware<ErrorHandlingMiddleware>(); 
app.UseMiddleware<RequestTimeLoggingMiddleware>(); 
app.UseSerilogRequestLogging();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//We can customize the api route for our identityEndpoint using MapGroup()
app.MapGroup("api/identity").WithTags("Identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();

// This is to make the program.cs accessible to other classes....
public partial class Program { }
