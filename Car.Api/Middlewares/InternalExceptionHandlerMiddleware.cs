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


