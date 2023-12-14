using CarWebService.BLL.Services.Contracts;

namespace CarWebService.API.Middlewares
{
    public class JwtRefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ITokenServices _tokenService;

        public JwtRefreshTokenMiddleware(RequestDelegate next, ITokenServices tokenService)
        {
            _next = next;
            _tokenService = tokenService;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.Headers.ContainsKey("IS-TOKEN-EXPIRED"))
            {
                var headerValue = context.Response.Headers["IS-TOKEN-EXPIRED"];

                if (headerValue == "true")
                {
                    string? refreshToken = context.Request.Cookies["refreshToken"];
                    //Если refresh token истек воз-ем heder с информацией об этом на клиент
                    if (refreshToken == null)
                    {
                        context.Response.Headers.Add("IS-REFRESHTOKEN-EXPIRED", "true");
                        return;
                    }
                }
            }
        }
    }
}
