using AutoMapper;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Services.Contracts;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;

namespace CarWebService.BLL.Services.Implementations
{
    /// <summary>
    /// Бизнес логика для пользоавтеля.
    /// </summary>
    public class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserServices(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Создание пользователя в БД.
        /// </summary>
        /// <param name="command">Данные пользователя.</param>
        public async Task CreateUser(UserDto command)
        {
            var user = _mapper.Map<User>(command);
            await _userRepository.CreateUser(user);
        }

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        /// <summary>
        /// Получение записей всех пользователей.
        /// </summary>
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var userDto = _mapper.Map<List<UserDto>>(users);

            return userDto;
        }

        /// <summary>
        /// Получение пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public async Task<UserDto> GetUserByid(int id)
        {
            var user = await _userRepository.GetUserByid(id);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        /// <summary>
        /// Присвоение роли "User" пользователю.
        /// </summary>
        /// <returns>Роль.</returns>
        public async Task<Role> GetDefaultRole()
        {
            var role = await _userRepository.GetDefaultRole();

            return role;
        }

        /// <summary>
        /// Получение роли по названию.
        /// </summary>
        /// <param name="name">Название роли.</param>
        /// <returns>Роль.</returns>
        public async Task<Role> GetRoleByName(string name)
        {
            var role = await _userRepository.GetRoleByName(name);

            return role;
        }

        /// <summary>
        /// Полкчение существующего пользователя.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <param name="password">Пароль пользоватлея.</param>
        /// <returns>Пользователь.</returns>
        public async Task<User> GetExistingUser(string email, string password)
        {
            var user = await _userRepository.GetExistingUser(email, password);

            return user;
        }
    }
}
