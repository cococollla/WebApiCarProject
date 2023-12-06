using Microsoft.AspNetCore.Authorization;

namespace CarWebService.API.Middlewares
{
    public class JwtAccessTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtAccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var endpoint = context.GetEndpoint();
            var authorizeAtribure = endpoint?.Metadata.GetMetadata<AuthorizeAttribute>();

            if (authorizeAtribure != null)
            {
                var accessToken = context.Request.Cookies["accessToken"];

                if (accessToken != null)
                {
                    context.Request.Headers.Authorization = $"Bearer {accessToken}";
                }
            }

            await _next.Invoke(context);
        }
    }
}
