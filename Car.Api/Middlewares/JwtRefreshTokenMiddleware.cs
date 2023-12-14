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
                    context.Request.Headers.Remove("Authorization");
                    string? refreshToken = context.Request.Cookies["refreshToken"];
                    string? role = context.Request.Cookies["role"];
                    //Если refresh token воз-ем heder с информацией об это на клиент
                    if (refreshToken == null)
                    {
                        context.Response.Headers.Add("IS-REFRESHTOKEN-EXPIRED", "true");
                        return;
                    }

                    var accessToken = _tokenService.CreateToken(role);
                    context.Response.Cookies.Delete("accessToken");
                    context.Response.Cookies.Append("accessToken", accessToken);
                    context.Response.Headers.Add("Authorization", "Bearer " + accessToken);
                }
            }
        }
    }
}
