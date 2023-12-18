using CarWebService.BLL.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
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
        /// Создание access token.
        /// </summary>
        /// <param name="role"></param>
        /// <returns>Access token</returns>
        public string CreateToken(string role)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim> { new Claim(ClaimTypes.Role, role) };

            var jwt = new JwtSecurityToken(
                    issuer: _configuration.GetSection("JWT").GetValue<string>("Issuer"),
                    audience: _configuration.GetSection("JWT").GetValue<string>("Audience"),
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(_configuration.GetSection("JWT").GetValue<double>("Lifetime"))),
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
        /// Создание rafresh token.
        /// </summary>
        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(randomNumber);
                string refreshToken = Convert.ToBase64String(randomNumber);
                return refreshToken;
            }
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
