using CarWebService.DAL.Common.Exceptions;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarWebService.DAL.Repositories.Implementations
{
    /// <summary>
    /// Репозитирой для управления записями пользователей в БД.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет пользователя в БД.
        /// </summary>
        /// <param name="request">Данные пользователя для регистрации.</param>
        public async Task CreateUser(User request)
        {
            await _context.Users.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет запись о пользователе из БД
        /// </summary>
        /// <param name="id">Идентификатор по которому будет удален пользователь.</param>
        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает список всех пользователей из БД.
        /// </summary>
        /// <returns>Список пользователей.</returns>
        public async Task<List<User>> GetAllUsers()
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();

            if (users == null)
            {
                throw new NotFoundException("Not found");
            }

            return users;
        }

        /// <summary>
        /// Получает запись о пользователе из БД.
        /// </summary>
        /// <param name="id">Идентификатор по которому будет найден пользователь.</param>
        /// <returns>Данные пользователя</returns>
        public async Task<User> GetUserByid(int id)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            return user;
        }

        /// <summary>
        /// Получает список всех ролей из БД.
        /// </summary>
        /// <returns>Список ролей.</returns>
        public async Task<List<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles;
        }

        /// <summary>
        /// Задает роль по-умолчанию для пользователя при регистрации.
        /// </summary>
        /// <returns>Идентификатор роли из БД.</returns>
        public async Task<Role> GetDefaultRole()
        {
            var role = await _context.Roles.FirstOrDefaultAsync(role => role.Name == "User");

            return role;
        }

        /// <summary>
        /// Получает роль по ее имени.
        /// </summary>
        /// <param name="name">Имя роли.</param>
        /// <returns>Роль.</returns>
        public async Task<Role> GetRoleByName(string name)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(role => role.Name == name);

            if (role == null)
            {
                return await _context.Roles.FirstOrDefaultAsync(role => role.Name == "User");
            }

            return role;
        }

        /// <summary>
        /// Получает пользователя по его имейлу и паролю.
        /// </summary>
        /// <param name="email">Почта пользователя.</param>
        /// <param name="password">Пароль пользователя.</param>
        /// <returns>Пользователь.</returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<User> GetExistingUser(string email, string password)
        {
            var user = _context.Users.Include(u => u.Role).FirstOrDefault(user => user.Email == email && user.Password == password);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            return user;
        }
    }
}
