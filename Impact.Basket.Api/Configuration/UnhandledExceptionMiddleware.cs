using Impact.Basket.Api.Models.Responses;
using Newtonsoft.Json;
using System.Net;

namespace Impact.Basket.Api.Configuration
{
    /// <summary>
    /// Middleware for handling unhandled exceptions.
    /// </summary>
    public class UnhandledExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<UnhandledExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Initializes a new instance of the UnhandledExceptionMiddleware class.
        /// </summary>
        /// <param name="next">The next request delegate.</param>
        /// <param name="logger">The logger used for logging.</param>
        /// <param name="environment">The hosting environment.</param>
        public UnhandledExceptionMiddleware(RequestDelegate next, ILogger<UnhandledExceptionMiddleware> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _next = next;
            _environment = environment;
        }

        /// <summary>
        /// Invokes the middleware to handle unhandled exceptions.
        /// </summary>
        /// <param name="httpContext">The HTTP context.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var handleExceptions = true;
            //var handleExceptions = _environment?.IsDevelopment() != true;

            try
            {
                await _next(httpContext);
            }
            catch (UnauthorizedAccessException ex) when (handleExceptions)
            {
                _logger.LogError(ex, "Unauthorised Access Exception: {message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.Unauthorized, ex.Message);
            }
            catch (Exception ex) when (handleExceptions)
            {
                _logger.LogError(ex, "Unhandled Exception: {message}", ex.Message);
                await HandleExceptionAsync(httpContext, ex, HttpStatusCode.InternalServerError);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, HttpStatusCode statusCode, string? message = null)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            var errorResponse = new ErrorResponse()
            {
                Code = context.Response.StatusCode,
                Message = message ?? "An unhandled exception occurred. Please try again or consult the administrator."
            };

            var errorResponseString = JsonConvert.SerializeObject(errorResponse);
            return context.Response.WriteAsync(errorResponseString);
        }
    }

}
