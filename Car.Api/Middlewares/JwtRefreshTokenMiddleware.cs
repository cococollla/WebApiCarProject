using System.IdentityModel.Tokens.Jwt;

namespace CarWebService.API.Middlewares
{
    public class JwtRefreshTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtRefreshTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.Headers.ContainsKey("IS-TOKEN-EXPIRED"))
            {
                var refreshToken = context.Request.Cookies["refreshToken"];
                var handler = new JwtSecurityTokenHandler();
                var jwtTokenRefrshToken = handler.ReadJwtToken(refreshToken);

                if (refreshToken == null)
                {
                    context.Response.Headers.Add("IS-REFRESHTOKEN-EXPIRED", "true");
                    return;
                }

                var refreshTokenValidTo = jwtTokenRefrshToken.ValidTo;

                if (refreshTokenValidTo < DateTime.UtcNow) //Если refresh token истек воз-ем heder с информацией об этом на клиент
                {
                    context.Response.Headers.Add("IS-REFRESHTOKEN-EXPIRED", "true");
                    return;
                }
            }
        }
    }
}