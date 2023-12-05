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

        private ActionResult<string> GetToken()
        {
            var token = _tokenServices.CreateToken("Admin");
            var refreshToken = _tokenServices.CreateRefreshToken();

            var cookieForRefrshToken = new CookieOptions //добавление refreshToken в куки на неделю
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7),
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieForRefrshToken);

            var cookieForAccessToken = new CookieOptions //добавление accessToken в куки на неделю
            {
                HttpOnly = true,
            };
            Response.Cookies.Append("refreshToken", refreshToken, cookieForAccessToken);

            var response = new AuthResponse
            {
                Role = "Admin",
                AccessToken = token,
                RefreshToken = refreshToken
            };

            return refreshToken;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Login(UserDto request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return NotFound();
            }

            var response = GetToken();

            return Ok(response);


        }

        [HttpPost]
        public async Task<ActionResult<User>> Signup(UserDto request)
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
