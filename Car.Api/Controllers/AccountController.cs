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

        public AccountController(UserManager<User> userManager, IUserServices userService, IMapper mapper, ITokenServices tokenServices, ISessionServices sessionServices, IConfiguration configuration)
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
        public async Task<IResult> Login(AuthRequest request)
        {
            var user = await _userService.GetExistingUser(request.Email, request.Password);

            if (user == null)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;//Явно присваиваем код ответа, т.к. Results.NotFound() вернет ответ с кодом 200, а NotFound 404 запишет в тело ответа
                return Results.NotFound();
            }

            var response = await GetToken(user);

            return Results.Json(response);
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="request">Данные пользователя для регистрации.</param>
        [HttpPost]
        public async Task<ActionResult<User>> Signup([FromForm] UserDto request)
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
        public async Task<IResult> RefreshToken()
        {
            var userId = int.Parse(Request.Headers["userId"]);
            var userDto = await _userService.GetUserByid(userId);
            var session = await _sessionServices.GetSessionByUserId(userId);
            var refreshTokenLifetime = _configuration.GetSection("JWT").GetValue<TimeSpan>("RefreshTokenLifetime");
            var refreshTokenCookie = Request.Cookies["refreshToken"];

            if (session.RefreshToken != refreshTokenCookie && session.ValidTo < DateTime.UtcNow)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;//Явно присваиваем код ответа, т.к. Results.NotFound() вернет ответ с кодом 200, а NotFound 404 запишет в тело ответа
                return Results.NotFound();
            }

            var refreshToken = _tokenServices.CreateRefreshToken(userDto.RoleName, userDto.Email);
            var accessToken = _tokenServices.CreateToken(userDto.RoleName, userDto.Email);

            session.RefreshToken = refreshToken;
            session.ValidTo = DateTime.UtcNow.Add(refreshTokenLifetime);

            var result = await _sessionServices.UpdateSession(session);

            if (!result)
            {
                Response.StatusCode = StatusCodes.Status404NotFound;//Явно присваиваем код ответа, т.к. Results.NotFound() вернет ответ с кодом 200, а NotFound 404 запишет в тело ответа
                return Results.NotFound();
            }

            Response.Cookies.Delete("refreshToken");

            var cookieForRefrshToken = new CookieOptions //Создание кук для рефреш токена
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.Add(refreshTokenLifetime),
                SameSite = SameSiteMode.None,
                Secure = true
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieForRefrshToken);//добавление refreshToken в куки на неделю

            return Results.Json(accessToken);
        }

        /// <summary>
        /// Реализует выход из аккаунта.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            var userId = int.Parse(Request.Headers["userId"]);
            await _sessionServices.DeleteSesion(userId);

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
            var refreshTokenLifetime = _configuration.GetSection("JWT").GetValue<TimeSpan>("RefreshTokenLifetime");

            var cookieForRefrshToken = new CookieOptions //Создание кук для рефреш токена
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.Add(refreshTokenLifetime),
                SameSite = SameSiteMode.None,
                Secure = true
            };

            Response.Cookies.Append("refreshToken", refreshToken, cookieForRefrshToken);//добавление refreshToken в куки на неделю

            var newSession = new Session
            {
                RefreshToken = refreshToken,
                ValidTo = DateTime.UtcNow.Add(refreshTokenLifetime),
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
