using BLL.Services.Contracts;
using BLL.Services.Models.DtoModels;
using DAL.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
            await _userServices.CreateUser(user);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto user)
        {
            try
            {
                await _userServices.UpdateUser(user);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetUsers()
        {
            var users = await _userServices.GetAllUsers();

            return Ok(users);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                await _userServices.DeleteUser(id);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
