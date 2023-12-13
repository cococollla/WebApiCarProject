using AutoMapper;
using CarWebService.API.Models;
using CarWebService.BLL.Services.Contracts;
using CarWebService.BLL.Services.Models.DtoModels;
using CarWebService.DAL.Common.Exceptions;
using CarWebService.DAL.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarWebService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserServices _userService;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, IUserServices userService, IMapper mapper, ITokenServices tokenServices)
        {
            _userManager = userManager;
            _userService = userService;
            _mapper = mapper;
            _tokenServices = tokenServices;
        }

        /// <summary>
        /// Выдает токен для аутентифицированного пользователя
        /// </summary>
        /// <param name="role">Роль пользователя</param>
        /// <returns>AccessToken, RefreshToken, Role</returns>
        private AuthResponse GetToken(string role)
        {
            var token = _tokenServices.CreateToken(role);
            var refreshToken = _tokenServices.CreateRefreshToken();

            var cookieForRefrshToken = new CookieOptions //добавление refreshToken в куки на неделю
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieForRefrshToken);

            var cookieForAccessToken = new CookieOptions //добавление accessToken в куки
            {
                HttpOnly = true,
            };
            Response.Cookies.Append("accessToken", token, cookieForAccessToken);

            var cookieForRole = new CookieOptions //добавление роль в куки
            {
                HttpOnly = true,
            };
            Response.Cookies.Append("role", role, cookieForRole);

            var response = new AuthResponse
            {
                Role = role,
                AccessToken = token,
                RefreshToken = refreshToken
            };

            return response;
        }

        /// <summary>
        /// Реализует вход в приложение
        /// </summary>
        /// <param name="request">Данные для входа</param>
        [HttpPost]
        public async Task<IResult> Login(AuthRequest request)
        {
            try
            {
                var user = await _userService.GetExistingUser(request.Email, request.Password);
                var response = GetToken(user.Role.Name);

                return Results.Json(response);
            }
            catch (NotFoundException)
            {
                return Results.StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception)
            {
                return Results.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="request">Данные пользователя для регистрации</param>
        [HttpPost]
        public async Task<ActionResult<User>> Signup([FromForm] UserDto request)
        {
            var user = _mapper.Map<User>(request);
            user.Role = await _userService.GetDefaultRole();
            var result = await _userManager.CreateAsync(user, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(Url.Action(nameof(Login)));
        }
    }
}
