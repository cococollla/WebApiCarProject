using CarWebService.BLL.Services.Models.DtoModels;
using CarWebService.DAL.Models.Entity;

namespace CarWebService.BLL.Services.Contracts
{
    public interface IUserServices
    {
        public Task CreateUser(UserDto command);
        public Task<List<UserDto>> GetAllUsers();
        public Task<UserDto> GetUserByid(int id);
        public Task DeleteUser(int id);
        public Task<Role> GetDefaultRole();
    }
}
