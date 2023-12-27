using CarWebService.BLL.Models.DtoModels;
using CarWebService.DAL.Models.Entity;

namespace CarWebService.BLL.Services.Contracts
{
    public interface IUserServices
    {
        /// <summary>
        /// Создание пользователя в БД.
        /// </summary>
        /// <param name="command">Данные пользователя.</param>
        public Task CreateUser(UserDto command);

        /// <summary>
        /// Получение записей всех пользователей.
        /// </summary>
        public Task<List<UserDto>> GetAllUsers();

        /// <summary>
        /// Получение пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public Task<UserDto> GetUserByid(int id);

        /// <summary>
        /// Удаление пользователя.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        public Task DeleteUser(int id);

        /// <summary>
        /// Присвоение роли "User" пользователю.
        /// </summary>
        /// <returns>Роль.</returns>
        public Task<Role> GetDefaultRole();

        /// <summary>
        /// Получение роли по названию.
        /// </summary>
        /// <param name="name">Название роли.</param>
        /// <returns>Роль.</returns>
        public Task<Role> GetRoleByName(string name);

        /// <summary>
        /// Получение существующего пользователя.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <param name="password">Пароль пользоватлея.</param>
        /// <returns>Пользователь.</returns>
        public Task<User> GetExistingUser(string email, string password);
    }
}