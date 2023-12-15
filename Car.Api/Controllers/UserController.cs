using AutoMapper;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Services.Contracts;
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

        public UserController(UserManager<User> userManager, IMapper mapper, IUserServices userServices)
        {
            _userManager = userManager;
            _mapper = mapper;
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto request)
        {
            var user = _mapper.Map<User>(request);
            user.Role = await _userServices.GetDefaultRole();
            var result = await _userManager.CreateAsync(user, user.Password);

            if (!result.Succeeded)
            {
                throw new Exception();
            }

            return Created($"{user.Id}", Url.Action(nameof(GetUserById), new { id = user.Id }));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userServices.GetUserByid(id);
                var userDto = _mapper.Map<UserDto>(user);

                return Ok(userDto);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            try
            {
                var users = await _userServices.GetAllUsers();

                if (users == null)
                {
                    throw new NotFoundException("Not found");
                }

                var usersDto = _mapper.Map<List<UserDto>>(users);

                return Ok(usersDto);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
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

                result.UserName = request.Name;
                result.Email = request.Email;//email должен быть написан по шаблону [...@...]
                result.Role = await _userServices.GetRoleByName(request.RoleName);

                var updateResult = await _userManager.UpdateAsync(result);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    throw new NotFoundException("User is not found");
                }

                await _userManager.DeleteAsync(user); //DeleteAsync userManger принимает объект User, поэтому приходится сначала найти пользователя по id

                return Ok();
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
