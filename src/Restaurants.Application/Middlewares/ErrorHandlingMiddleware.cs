using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Exceptions;


namespace Restaurants.Application.Middlewares
{
    public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger) : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFound.Message);

                _logger.LogWarning(notFound.Message);   
            }
            catch (ConflictException conflict)
            {
                context.Response.StatusCode = 409;
                await context.Response.WriteAsync(conflict.Message);

                _logger.LogWarning(conflict.Message);
            }
            catch (ForbiddenException forbidden)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(forbidden.Message);

                _logger.LogWarning(forbidden.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
