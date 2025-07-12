using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;
using Restaurants.Application.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;
           // services.AddScoped<IRestaurantsService, RestaurantsService>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(applicationAssembly));

            services.AddAutoMapper(applicationAssembly);

            services.AddValidatorsFromAssembly(applicationAssembly).AddFluentValidationAutoValidation();

            //Register the User Context that get the user claims property from the current httpContext
            services.AddScoped<IUserContext, UserContext>();

            //To use the httpContextAccessor we will add: (in order to access the userClaims)
            services.AddHttpContextAccessor();
        }
    }
}
