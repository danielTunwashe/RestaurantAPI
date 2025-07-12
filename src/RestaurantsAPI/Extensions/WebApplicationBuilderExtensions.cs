using Microsoft.OpenApi.Models;
using Restaurants.Application.Extensions;
using Restaurants.Application.Middlewares;
using Restaurants.Infrastructure.Extensions;
using Serilog;

namespace RestaurantsAPI.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddPresentation(this WebApplicationBuilder builder)
        {
            
            //Create a custom error habdling middle ware services to track things that happens during request processing, 
            //This is the registration of the middleware depedency
            builder.Services.AddScoped<ErrorHandlingMiddleware>();
            builder.Services.AddScoped<RequestTimeLoggingMiddleware>();
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            //Enables us to store some access token in memeory to use in followup request
            //Brings the beautiful Authorize bar where we can store our access token
            builder.Services.AddSwaggerGen(c =>
            {
                //Create the Authorize swaggerpop Up
                //Enables us to store some access token in memeory to use in followup request
                //Brings the beautiful Authorize bar where we can store our access token
                c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                //Enables us to inject the access token in our request
                //So swagger UI knows that the access token will be used by the request.
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference{Type = ReferenceType.SecurityScheme, Id= "bearerAuth"}
                        },
                        []
                    }
                });
             });

            //Now this allows us to Authenticate a user by its token which is passed as an header to evry request made by the user after loggin in
            builder.Services.AddAuthentication();

            builder.Host.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });
        }
    }
}
