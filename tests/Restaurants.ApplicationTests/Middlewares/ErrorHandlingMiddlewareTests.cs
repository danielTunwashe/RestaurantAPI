using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using System.Threading.Tasks;
using Xunit;


namespace Restaurants.Application.Middlewares.Tests
{
    public class ErrorHandlingMiddlewareTests
    {
        [Fact()]
        public async Task InvokeAsync_WhenNoExceptionThrown_ShouldCallNextDelegate()
        {
            //arrange
            // Creating a mock for the ILogger<ErrorHandlingMiddleware>
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            // Creating an instance of the ErrorHandlingMiddleware with the mocked logger
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);

            // Creating a new HttpContext for testing
            var context = new DefaultHttpContext();

            // Creating a mock for the RequestDelegate
            var nextDelegateMock = new Mock<RequestDelegate>();

            //act
            // Invoking the middleware with the context and the mocked next delegate
            await middleware.InvokeAsync(context, nextDelegateMock.Object);

            //assert
            // Verifying that the next delegate was called once
            nextDelegateMock.Verify(next => next.Invoke(context), Times.Once());

        }

        [Fact()]
        public async Task InvokeAsync_WhenNotFoundExceptionThrown_ShouldSetStatusCode404()
        {
            //arrange

            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var notFoundException = new NotFoundException(nameof(Restaurant), "1");

            //act
            await middleware.InvokeAsync(context, _ => throw notFoundException);    


            //assert
            context.Response.StatusCode.Should().Be(404);
        }


        [Fact()]
        public async Task InvokeAsync_WhenForbidExceptionThrown_ShouldSetStatusCode403()
        {
            //arrange

            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var exception = new ForbiddenException();

            //act
            await middleware.InvokeAsync(context, _ => throw exception);


            //assert
            context.Response.StatusCode.Should().Be(403);
        }


        [Fact()]
        public async Task InvokeAsync_WhenConflictExceptionThrown_ShouldSetStatusCode409()
        {
            //arrange

            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var exception = new ConflictException(nameof(Restaurant), "1");

            //act
            await middleware.InvokeAsync(context, _ => throw exception);


            //assert
            context.Response.StatusCode.Should().Be(409);
        }


        [Fact()]
        public async Task InvokeAsync_WhenGenericExceptionThrown_ShouldSetStatusCode500()
        {
            //arrange

            var context = new DefaultHttpContext();
            var loggerMock = new Mock<ILogger<ErrorHandlingMiddleware>>();
            var middleware = new ErrorHandlingMiddleware(loggerMock.Object);
            var exception = new Exception();

            //act
            await middleware.InvokeAsync(context, _ => throw exception);


            //assert
            context.Response.StatusCode.Should().Be(500);
        }

    }
}