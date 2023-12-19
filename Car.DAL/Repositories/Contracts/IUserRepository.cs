using CarWebService.DAL.Models.Entity;

namespace CarWebService.DAL.Repositories.Contracts
{
    public interface IUserRepository
    {
        /// <summary>
        /// Добавляет пользователя в БД.
        /// </summary>
        /// <param name="request">Данные пользователя для регистрации.</param>
        public Task CreateUser(User request);

        /// <summary>
        /// Получает список всех пользователей из БД.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public Task<List<User>> GetAllUsers();

        /// <summary>
        /// Получает запись о пользователе из БД.
        /// </summary>
        /// <param name="id">Идентификатор по которому будет найден пользователь.</param>
        /// <returns>Данные пользователя.</returns>
        public Task<User> GetUserByid(int id);

        /// <summary>
        /// Удаляет запись о пользователе из БД
        /// </summary>
        /// <param name="id">Идентификатор по которому будет удален пользователь.</param>
        public Task DeleteUser(int id);

        /// <summary>
        /// Получает список всех ролей из БД.
        /// </summary>
        /// <returns>Список ролей.</returns>
        public Task<List<Role>> GetRoles();

        /// <summary>
        /// Задает роль по-умолчанию для пользователя при регистрации.
        /// </summary>
        /// <returns>Идентификатор роли из БД.</returns>
        public Task<Role> GetDefaultRole();

        /// <summary>
        /// Получает роль по ее имени.
        /// </summary>
        /// <param name="name">Имя роли.</param>
        /// <returns>Роль.</returns>
        public Task<Role> GetRoleByName(string name);

        /// <summary>
        /// Получает пользователя по его имейлу и паролю.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Пользователь.</returns>
        public Task<User> GetExistingUser(string email, string password);
    }
}
