using AutoMapper;
using CarWebService.API.Models;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Services.Contracts;
using CarWebService.DAL.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarWebService.API.Controllers
{
    /// <summary>
    /// Контроллер для регистрации и авторизации.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserServices _userService;
        private readonly ITokenServices _tokenServices;
        private readonly ISessionServices _sessionServices;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager,
            IUserServices userService,
            IMapper mapper,
            ITokenServices tokenServices,
            ISessionServices sessionServices,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            _tokenServices = tokenServices;
            _sessionServices = sessionServices;
            _configuration = configuration;
        }

        /// <summary>
        /// Реализует вход в аккаунт.
        /// </summary>
        /// <param name="request">Данные для входа.</param>
        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest request)
        {
            var user = await _userService.GetExistingUser(request.Email, request.Password);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var response = await GetToken(user);

            return Ok(response);
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="request">Данные пользователя для регистрации.</param>
        [HttpPost]
        public async Task<ActionResult<User>> Signup(UserDto request)
        {
            var user = _mapper.Map<User>(request);
            user.Role = await _userService.GetDefaultRole();
            var result = await _userManager.CreateAsync(user, user.Password);

            if (!result.Succeeded)//Возникает, если не удалось подключиться к БД или невалидные данные пользователя
            {
                return BadRequest(result.Errors);
            }

            return Ok(Url.Action(nameof(Login)));
        }

        /// <summary>
        /// Обновляет истекший access token.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> RefreshToken()
        {
            var userId = int.Parse(Request.Headers["userId"]);
            var user = await _userService.GetUserById(userId);
            var session = await _sessionServices.GetSessionByUserId(userId);
            var refreshTokenLifetime = _configuration.GetSection("JWT").GetValue<TimeSpan>("RefreshTokenLifetime");
            var refreshTokenCookie = HttpContext.Request.Cookies["refreshToken"];
            //var refreshTokenCookie = Request.Cookies["refreshToken"];

            if (session.RefreshToken != refreshTokenCookie || session.ValidTo < DateTime.UtcNow)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var refreshToken = _tokenServices.CreateRefreshToken(user.RoleName, user.Email);
            var accessToken = _tokenServices.CreateToken(user.RoleName, user.Email);
            session.RefreshToken = refreshToken;
            session.ValidTo = DateTime.UtcNow.Add(refreshTokenLifetime);

            var result = await _sessionServices.UpdateSession(session);

            if (!result)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            Response.Cookies.Delete("refreshToken");

            var cookieForRefrshToken = new CookieOptions //Создание кук для рефреш токена
            {
                HttpOnly = true,
                Expires = session.ValidTo,
                SameSite = SameSiteMode.None,
                Secure = true
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieForRefrshToken);//добавление refreshToken в куки на неделю

            return Ok(accessToken);
        }

        /// <summary>
        /// Реализует выход из аккаунта.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var userId = int.Parse(Request.Headers["userId"]);
            await _sessionServices.DeleteSession(userId);

            Response.Cookies.Delete("refreshToken");

            return NoContent();
        }

        /// <summary>
        /// Выдает токен для аутентифицированного пользователя.
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>AccessToken, Роль пользователя, Идентификатор пользователя.</returns>
        private async Task<AuthResponse> GetToken(User user)
        {
            var accessToken = _tokenServices.CreateToken(user.Role.Name, user.Email);
            var refreshToken = _tokenServices.CreateRefreshToken(user.Role.Name, user.Email);
            var refreshTokenValidTo = DateTime.UtcNow.Add(_configuration.GetSection("JWT").GetValue<TimeSpan>("RefreshTokenLifetime"));

            var cookieForRefrshToken = new CookieOptions //Создание кук для рефреш токена
            {
                HttpOnly = true,
                Expires = refreshTokenValidTo,
                SameSite = SameSiteMode.None,
                Secure = true
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieForRefrshToken);//добавление refreshToken в куки на неделю

            var newSession = new Session
            {
                RefreshToken = refreshToken,
                ValidTo = refreshTokenValidTo,
                User = user,
                UserId = user.Id
            };

            var response = new AuthResponse
            {
                Role = user.Role.Name,
                AccessToken = accessToken,
                UserId = user.Id
            };

            var session = await _sessionServices.GetSessionByUserId(user.Id);

            if (session != null)
            {
                await _sessionServices.UpdateSession(newSession);

                return response;
            }

            await _sessionServices.CreateSession(newSession);

            return response;
        }
    }
}