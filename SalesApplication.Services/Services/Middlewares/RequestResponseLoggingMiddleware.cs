
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text;


namespace SalesApplication.Services.Services.Middlewares
{
    /// <summary>
    /// RequestResponseLogging Cusotm Middleware
    /// </summary>
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;
        public RequestResponseLoggingMiddleware(
            RequestDelegate next,
            ILogger<RequestResponseLoggingMiddleware> logger )
        {
            _next = next;
            _logger = logger;

        }

        public async Task Invoke(HttpContext context)
        {
            // Log the incoming request
            LogRequest(context.Request);

            // Call the next middleware in the pipeline
            await _next(context);

            // Log the outgoing response
            LogResponse(context.Response);
        }

        private void LogRequest(HttpRequest request)
        {
            _logger.LogInformation("Request received: {Method} {Path}", request.Method, request.Path);
            _logger.LogInformation("Request headers: {Headers}", GetHeadersAsString(request.Headers));
        }

        private void LogResponse(HttpResponse response)
        {
            _logger.LogInformation("Response sent: {StatusCode}", response.StatusCode);
            _logger.LogInformation("Response headers: {Headers}", GetHeadersAsString(response.Headers));
        }

        private string GetHeadersAsString(IHeaderDictionary headers)
        {
            var stringBuilder = new StringBuilder();
            foreach (var (key, value) in headers)
            {
                stringBuilder.AppendLine($"{key}: {value}");
            }
            return stringBuilder.ToString();
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestResponseLoggingMiddleware>();
        }
    }
}
