using AutoMapper;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Services.Contracts;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;

namespace CarWebService.BLL.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserServices(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task CreateUser(UserDto command)
        {
            var user = _mapper.Map<User>(command);
            await _userRepository.CreateUser(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsers();
            var userDto = _mapper.Map<List<UserDto>>(users);

            return userDto;
        }

        public async Task<UserDto> GetUserByid(int id)
        {
            var user = await _userRepository.GetUserByid(id);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<Role> GetDefaultRole()
        {
            var role = await _userRepository.GetDefaultRole();
            return role;
        }

        public async Task<Role> GetRoleByName(string name)
        {
            var role = await _userRepository.GetRoleByName(name);

            return role;
        }

        public async Task<User> GetExistingUser(string email, string password)
        {
            var user = await _userRepository.GetExistingUser(email, password);

            return user;
        }
    }
}
