using AutoMapper;
using CarWebService.API.Models;
using CarWebService.BLL.Services.Contracts;
using CarWebService.BLL.Services.Models.DtoModels;
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
        private readonly IUserServices _userService;
        private readonly ITokenServices _tokenServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<User> userManager, IUserServices userService, SignInManager<User> signInManager, IMapper mapper, ITokenServices tokenServices)
        {
            _userManager = userManager;
            _userService = userService;
            _signInManager = signInManager;
            _mapper = mapper;
            _tokenServices = tokenServices;
        }

        private async Task<AuthResponse> GetToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);//Поиск роли для дальнейшего добавления в Clims
            var token = _tokenServices.CreateToken(user.Role.Name);
            var refreshToken = _tokenServices.CreateRefreshToken();

            var cookieOptions = new CookieOptions //добавление refreshToken в куки на неделю
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

            var response = new AuthResponse
            {
                Role = user.Role.Name,
                AccessToken = token,
                RefreshToken = refreshToken
            };

            return response;
        }

        [HttpGet]
        public async Task<IResult> Login(UserDto request)
        {
            var user = _mapper.Map<User>(request);
            var result = await _signInManager.PasswordSignInAsync(user.Email, user.Password, true, false);

            if (!result.Succeeded)
            {
                return Results.BadRequest("Incorrect email or password");
            }

            var response = GetToken(user.Email);

            return Results.Json(response);


        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Signup(UserDto request)
        {

            var user = _mapper.Map<User>(request);
            user.Role = await _userService.GetDefaultRole();
            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Created($"/api/User/GetUserById/{user.Id}", user);
        }
    }
}
