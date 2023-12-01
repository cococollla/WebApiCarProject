using CarWebService.DAL.Common.Exceptions;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarWebService.DAL.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Добавляет пользователя в БД
        /// </summary>
        /// <param name="request">Данные пользователя для регистрации</param>
        public async Task CreateUser(User request)
        {
            await _context.Users.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет запись о пользователе из БД
        /// </summary>
        /// <param name="id">Id по которому будет удален пользователь</param>
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
        /// Получает список всех пользователей из БД
        /// </summary>
        /// <returns>Список пользователей</returns>
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
        /// Получает запись о пользователе из БД
        /// </summary>
        /// <param name="id">Id по которому будет найден пользователь</param>
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
        /// Получает роль пользователя по его имени
        /// </summary>
        /// <param name="name">Логин по которому будет найден пользователь</param>
        /// <returns>Данные пользователя</returns>
        public async Task<User> GetUserByLogin(string login)
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(user => user.Login == login);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            return user;
        }

        /// <summary>
        /// Обновляет данные пользоваетля в БД
        /// </summary>
        /// <param name="request">Обновленные данные</param>
        public async Task UpdateUser(User request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == request.Id);

            if (user == null)
            {
                throw new NotFoundException("User is not found");
            }

            user.Name = request.Name;
            user.Email = request.Email;
            user.Login = request.Login;
            user.Password = request.Password;
            user.RoleId = request.RoleId;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает список всех ролей из БД
        /// </summary>
        /// <returns>Список ролей</returns>
        public async Task<List<Role>> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();

            return roles;
        }

        /// <summary>
        /// Проверяет существует ли пользователь с данным логином
        /// </summary>
        /// <param name="login">Логин пользователя</param>
        public async Task<bool> IsExistUser(string login)
        {
            return await _context.Users.AnyAsync(user => user.Login == login);
        }

        /// <summary>
        /// Задает роль по-умолчанию для пользователя при регистрации
        /// </summary>
        /// <returns>Id роли из БД</returns>
        public int GetDefaultRole()
        {

            var roleId = _context.Roles.FirstOrDefaultAsync(role => role.Name == "User").Id;

            return roleId;
        }
    }
}
