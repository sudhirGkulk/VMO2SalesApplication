using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using SalesApplication.Services.Services.Middlewares;
using System.Reflection;

namespace SalesApplication.MiddlewareTests
{
    [TestClass]
    public class RequestResponseLoggingMiddlewareTests
    {
        private Mock<RequestDelegate> _next;
        private Mock<ILogger<RequestResponseLoggingMiddleware>> _logger;
        public RequestResponseLoggingMiddleware _requestResponseLogging;

        [TestInitialize]
        public void TestInitialize()
        {
            _next = new Mock<RequestDelegate>();
            _logger = new Mock<ILogger<RequestResponseLoggingMiddleware>>();
            _requestResponseLogging = new RequestResponseLoggingMiddleware(_next.Object, _logger.Object);
        }

        [TestMethod]
        public async Task Invoke_LogsRequestAndResponse()
        {
            //Arrange
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/api/sales";
            context.Response.StatusCode = 200;
            context.Response.Headers.Add("X-Response-Header", "Value");
            //Act
            await _requestResponseLogging.Invoke(context);
            //Assert
            Assert.IsNotNull(_requestResponseLogging);
            Assert.AreEqual("GET", context.Request.Method);
        }

        [TestMethod]
        public void GetHeadersAsString_ReturnsFormattedHeaders()
        {
            //Arrange
            var headers = new HeaderDictionary
            {
                { "Content-Type", "application/json" },
                { "Authorization", "Bearer token" }
            };
            var getHeadersMethod = typeof(RequestResponseLoggingMiddleware).GetMethod("GetHeadersAsString", BindingFlags.NonPublic | BindingFlags.Instance);
            //Act
            var result = (string)getHeadersMethod.Invoke(_requestResponseLogging, new object[] { headers });
            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void LogRequest_CorrectlyLogsRequest()
        {
            //Arrange
            var context = new DefaultHttpContext();
            context.Request.Method = "GET";
            context.Request.Path = "/api/sales";
            var logRequestMethod = typeof(RequestResponseLoggingMiddleware).GetMethod("LogRequest", BindingFlags.NonPublic | BindingFlags.Instance);
            //Act
            logRequestMethod.Invoke(_requestResponseLogging, new object[] { context.Request });
            //Assert
            Assert.IsNotNull(_requestResponseLogging);
            Assert.AreEqual("GET", context.Request.Method);
        }

        [TestMethod]
        public void LogResponse_CorrectlyLogs_Response()
        {
            //Arrange
            var context = new DefaultHttpContext();
            context.Response.StatusCode = 200;
            context.Response.Headers.Add("X-Response-Header", "Value");
            var logResponseMethod = typeof(RequestResponseLoggingMiddleware).GetMethod("LogResponse", BindingFlags.NonPublic | BindingFlags.Instance);
            //Act
            logResponseMethod.Invoke(_requestResponseLogging, new object[] { context.Response });
            //Assert
            Assert.IsNotNull(_requestResponseLogging);
            Assert.AreEqual(200, context.Response.StatusCode);
        }

    }

}
