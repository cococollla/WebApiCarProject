using AutoMapper;
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
    public class UserController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IUserServices _userServices;
        private readonly RoleManager<Role> _roleManager;

        public UserController(UserManager<User> userManager, IMapper mapper, IUserServices userServices, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userServices = userServices;
            _roleManager = roleManager;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto request)
        {
            try
            {
                var user = _mapper.Map<User>(request);
                user.Role = await _userServices.GetDefaultRole(); //сделать метод для выдачи роли по имени из модели CarDto
                var result = await _userManager.CreateAsync(user, user.Password);

                if (!result.Succeeded)
                {
                    throw new Exception();
                }

                return Created($"/api/User/GetUserById/{user.Id}", user);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("id")]
        public async Task<ActionResult<User>> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    throw new NotFoundException("User is not found");
                }

                return Ok(user);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public ActionResult GetUsers()
        {
            try
            {
                var users = _mapper.Map<List<UserDto>>(_userManager.Users);

                if (users == null)
                {
                    throw new NotFoundException("Not found");
                }

                return Ok(users);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto request)
        {
            try
            {
                var user = _mapper.Map<User>(request);
                var result = await _userManager.FindByIdAsync(user.Id.ToString()); //Также возможно стоит использовать метод репозитория

                if (result == null)
                {
                    throw new NotFoundException("User is not found");
                }
                var role = await _roleManager.FindByNameAsync(request.RoleName);

                result.UserName = request.Name;
                result.Email = request.Email;//email должен быть написан по шаблону [...@...]
                result.Role = role;

                var updateResult = await _userManager.UpdateAsync(result);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    throw new NotFoundException("User is not found");
                }

                await _userManager.DeleteAsync(user); //DeleteAsync userManger принимает модель User, поэтому приходится сначала найти пользователя по id
                                                      //Есть сымыс использовать метод репозитория

                return Ok();
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
