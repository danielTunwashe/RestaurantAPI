

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Restaurants.Application.Middlewares
{
    public class RequestTimeLoggingMiddleware(ILogger<RequestTimeLoggingMiddleware> logger) : IMiddleware
    {
        private readonly ILogger<RequestTimeLoggingMiddleware> _logger = logger;
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();
            await next.Invoke(context);
            stopwatch.Stop();

            if(stopwatch.ElapsedMilliseconds / 1000 > 4)
            {
                _logger.LogInformation("Request [{verb}] at {path} took {Time} ms", context.Request.Method, context.Request.Path, stopwatch.ElapsedMilliseconds);
            } 
        }
    }
}
