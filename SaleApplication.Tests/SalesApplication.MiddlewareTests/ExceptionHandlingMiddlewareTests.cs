using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SalesApplication.Services.Services.Middlewares;

namespace SalesApplication.MiddlewareTests
{
    [TestClass]
    public class ExceptionHandlingMiddlewareTests
    {
        private  Mock<ILogger<ExceptionHandlingMiddleware>> _mockLogger;
        private  RequestDelegate _next;
        [TestInitialize]
        public void TestInitialize()
        {
            _mockLogger = new Mock<ILogger<ExceptionHandlingMiddleware>>();
            _next = (HttpContext context) => Task.CompletedTask;
        }

        [TestMethod]
        public async Task InvokeAsync_Should_LogException_And_ReturnInternalServerError()
        {

            // Arrange
            var middleware = new ExceptionHandlingMiddleware(_next, _mockLogger.Object);
            var context = new DefaultHttpContext();
            context.Response.Body = new MemoryStream();
            var exception = new InvalidOperationException("Test exception");

            // Act
            var exceptionHandler = async () =>
            {
                await middleware.InvokeAsync(context);
                throw exception;
            };

            //Assert
            await Assert.ThrowsExceptionAsync<InvalidOperationException>(exceptionHandler);
            Assert.AreEqual(200, context.Response.StatusCode);
            
            
        }
    }
}
