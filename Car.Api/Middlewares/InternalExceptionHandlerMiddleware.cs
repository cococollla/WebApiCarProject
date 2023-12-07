using Microsoft.IdentityModel.Tokens;
using NLog;

namespace CarWebService.API.Middlewares
{
    public class InternalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly NLog.ILogger _logger;

        public InternalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException ex)
            {
                var logMessage = $"Unauthorized access occurred in {context.Request.Method} {context.Request.Path}: {ex.Message}";
                _logger.Error(logMessage, ex);

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized error");

            }
            catch (SecurityTokenValidationException ex)
            {
                var logMessage = $"Forbidden access occurred in {context.Request.Method} {context.Request.Path}: {ex.Message}";
                _logger.Error(logMessage, ex);

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Forbidden error");
            }
            catch (Exception ex)
            {
                var logMessage = $"An unhandled exception occurred in {context.Request.Method} {context.Request.Path}: {ex.Message}";
                _logger.Error(ex, logMessage);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("Internal Server Error");
            }
        }
    }
}


