using CarWebService.BLL.Services.Contracts;
using CarWebService.BLL.Services.Models.DtoModels;
using CarWebService.DAL.Models.Entity;
using Microsoft.AspNetCore.Authorization;
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

        public AccountController(UserManager<User> userManager, IUserServices userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _userService.GetDefaultRole();
            var result = await _userManager.CreateAsync(
                new User() { UserName = user.Name, Email = user.Email, Role = role },
                user.Password
            );

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            user.Password = null;
            return Created("", result);
        }

        [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<UserDto>> GetUser(string username)
        {
            User user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return NotFound();
            }

            return new UserDto
            {
                Name = user.UserName,
                Email = user.Email
            };
        }
    }
}
