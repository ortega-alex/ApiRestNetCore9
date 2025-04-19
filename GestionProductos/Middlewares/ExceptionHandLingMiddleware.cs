using System.ComponentModel.DataAnnotations;

namespace GestionProductos.Middlewares
{
    public class ExceptionHandLingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandLingMiddleware> _logger;

        public ExceptionHandLingMiddleware(RequestDelegate next, ILogger<ExceptionHandLingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = exception switch
            {
                ValidationException => StatusCodes.Status400BadRequest,
                KeyNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                _ => StatusCodes.Status500InternalServerError
            };

            var errorResponse = new
            {
                statusCode,
                message = exception.Message,
                errorType = exception.GetType().Name,
                timestamp = DateTime.UtcNow,
                traceId = context.TraceIdentifier
            };

            // Log con estructura (útil para filtros en Seq)
            _logger.LogError(exception, "[{EventType}] {Message} | TraceId: {TraceId}",
                exception.GetType().Name,
                exception.Message,
                context.TraceIdentifier
            );

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsJsonAsync(errorResponse);
        }
    }
}
