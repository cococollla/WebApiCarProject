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
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUserServices _userService;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, IUserServices userService, SignInManager<User> signInManager, IMapper mapper, ITokenServices tokenServices, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenServices = tokenServices;
            _roleManager = roleManager;
        }

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

            var cookieForAccessToken = new CookieOptions //добавление accessToken
            {
                HttpOnly = true,
            };
            Response.Cookies.Append("accessToken", token, cookieForAccessToken);

            var response = new AuthResponse
            {
                Role = role,
                AccessToken = token,
                RefreshToken = refreshToken
            };

            return response;
        }

        [HttpGet]
        public async Task<IResult> Login(UserDto request)
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

        [HttpPost]
        public async Task<ActionResult<User>> Signup(UserDto request)
        {

            var user = _mapper.Map<User>(request);
            user.Role = await _userService.GetDefaultRole();
            var result = await _userManager.CreateAsync(user, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Created($"/api/User/GetUserById/{user.Id}", user);
        }
    }
}
