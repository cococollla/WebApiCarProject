using AutoMapper;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Services.Contracts;
using CarWebService.DAL.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarWebService.API.Controllers
{
    /// <summary>
    /// Контроллер для управления и просмотра пользователей.
    /// </summary>
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

        /// <summary>
        /// Создание пользователя в БД.
        /// </summary>
        /// <param name="request">Данные пользователя.</param>
        [HttpPost]
        public async Task<ActionResult<User>> CreateUser(UserDto request)
        {
            var user = _mapper.Map<User>(request);
            user.Role = await _userServices.GetDefaultRole();
            var result = await _userManager.CreateAsync(user, user.Password);

            if (!result.Succeeded)//Возникает, если не удалось подключиться к БД или невалидные данные пользователя
            {
                throw new Exception();
            }

            return Created($"{user.Id}", Url.Action(nameof(GetUserById), new { id = user.Id }));
        }

        /// <summary>
        /// Поиск записи пользователя по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя для поиска.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userServices.GetUserByid(id);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var userDto = _mapper.Map<UserDto>(user);

            return Ok(userDto);
        }

        /// <summary>
        /// Вывод записей всех пользователей.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var users = await _userServices.GetAllUsers();

            if (users == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var usersDto = _mapper.Map<List<UserDto>>(users);

            return Ok(usersDto);
        }

        /// <summary>
        /// Обновление записи пользователя в БД.
        /// </summary>
        /// <param name="request">Данные для обновления.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserDto request)
        {
            var user = _mapper.Map<User>(request);
            var result = await _userManager.FindByIdAsync(user.Id.ToString()); //Также возможно стоит использовать метод репозитория

            if (result == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            result.UserName = request.Name;
            result.Email = request.Email;//email должен быть написан по шаблону [...@...]
            result.Role = await _userServices.GetRoleByName(request.RoleName);

            await _userManager.UpdateAsync(result);

            return NoContent();
        }

        /// <summary>
        /// Удаления пользователя из БД.
        /// </summary>
        /// <param name="id">Идентфикатор пользователя для удаления.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            await _userManager.DeleteAsync(user); //DeleteAsync userManger принимает объект User, поэтому приходится сначала найти пользователя по id

            return Ok();
        }
    }
}