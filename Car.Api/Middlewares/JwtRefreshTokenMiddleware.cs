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
                const string loginPath = "/api/Account/Login";
                var headerValue = context.Response.Headers["IS-TOKEN-EXPIRED"];

                if (headerValue == "true")
                {
                    var path = context.Request.Path;
                    context.Request.Headers.Remove("Authorization");
                    string? refreshToken = context.Request.Cookies["refreshToken"];
                    string? role = context.Request.Cookies["role"];
                    //Если refresh token истек логинимся заново
                    if (refreshToken == null)
                    {
                        context.Request.Path = loginPath;
                        context.Response.Redirect(loginPath);
                        return;
                    }

                    var accessToken = _tokenService.CreateToken(role);
                    context.Response.Cookies.Delete("accessToken");
                    context.Response.Cookies.Append("accessToken", accessToken);
                    context.Response.Headers.Add("Authorization", "Bearer " + accessToken);
                    context.Response.Redirect($"{path}");
                }
            }
        }
    }
}
