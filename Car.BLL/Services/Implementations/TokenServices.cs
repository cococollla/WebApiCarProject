using CarWebService.BLL.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CarWebService.BLL.Services.Implementations
{
    /// <summary>
    /// Скрвис для работы с JWT токенами.
    /// </summary>
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;

        public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Создание access токена.
        /// </summary>
        /// <param name="role">Роль пользователя.</param>
        /// <param name="email">Электронная почта пользователя.</param>
        public string CreateToken(string role, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Email, email)
            };

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("JWT").GetValue<string>("Issuer"),
                    audience: _configuration.GetSection("JWT").GetValue<string>("Audience"),
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(_configuration.GetSection("JWT").GetValue<TimeSpan>("Lifetime")),
                    signingCredentials: CreateSigningCredentials()
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// Создание ключа.
        /// </summary>
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes("carsupersecretsuper_secretkey!123789"));
        }

        /// <summary>
        /// Создание refresh токена.
        /// </summary>
        /// <param name="role">Роль пользователя.</param>
        /// <param name="email">Электронная почта пользователя.</param>
        public string CreateRefreshToken(string role, string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Email, email)
            };

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("JWT").GetValue<string>("Issuer"),
                    audience: _configuration.GetSection("JWT").GetValue<string>("Audience"),
                    notBefore: DateTime.UtcNow,
                    claims: claims,
                    expires: DateTime.UtcNow.Add(_configuration.GetSection("JWT").GetValue<TimeSpan>("RefreshTokenLifetime")),
                    signingCredentials: CreateSigningCredentials()
                    );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        /// <summary>
        /// Создание криптографической подписи токена.
        /// </summary>
        private SigningCredentials CreateSigningCredentials()
        {
            return new SigningCredentials(
                GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256
            );
        }
    }
}
