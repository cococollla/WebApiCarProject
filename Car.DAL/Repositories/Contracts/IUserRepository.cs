using CarWebService.DAL.Models.Entity;

namespace CarWebService.DAL.Repositories.Contracts
{
    public interface IUserRepository
    {
        public Task CreateUser(User request);
        public Task UpdateUser(User request);
        public Task<List<User>> GetAllUsers();
        public Task<User> GetUserByid(int id);
        public Task DeleteUser(int id);
        public Task<List<Role>> GetRoles();
        public Task<bool> IsExistUser(string login);
        public Task<User> GetUserByLogin(string login);
        public int GetDefaultRole();
    }
}
