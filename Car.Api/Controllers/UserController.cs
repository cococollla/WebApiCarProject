using CarWebService.BLL.Services.Contracts;
using CarWebService.BLL.Services.Models.DtoModels;
using CarWebService.DAL.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace CarWebService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserServices _userServices;

        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto user)
        {
            try
            {
                await _userServices.CreateUser(user);

                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            try
            {
                await _userServices.UpdateUser(user);

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

        [HttpGet("id")]
        public async Task<ActionResult<CarDto>> GetUserById(int id)
        {
            try
            {
                var user = await _userServices.GetUserByid(id);

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

        [HttpGet("login")]
        public async Task<ActionResult<CarDto>> GetUserByLogin(string login)
        {
            try
            {
                var user = await _userServices.GetUserByLogin(login);

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
        public async Task<ActionResult<List<CarDto>>> GetUsers()
        {
            try
            {
                var users = await _userServices.GetAllUsers();

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

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userServices.DeleteUser(id);

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

        [HttpGet]
        public async Task<IActionResult> UserIsExist(string login)
        {
            bool isExist = await _userServices.IsExistUser(login);

            return Ok(isExist);
        }
    }
}
