using BLL.Services.Models.DtoModels;
using DAL.Models.Entity;

namespace BLL.Services.Contracts
{
    public interface IUserServices
    {
        public Task CreateUser(UserDto command);
        public Task UpdateUser(UserDto command);
        public Task<List<UserDto>> GetAllUsers();
        public Task<UserDto> GetUserByid(int id);
        public Task DeleteUser(int id);
        public Task<bool> IsExistUser(string login);
        public Task<UserDto> GetUserByLogin(string login);
    }
}
