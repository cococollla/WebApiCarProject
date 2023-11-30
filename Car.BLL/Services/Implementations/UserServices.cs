﻿using AutoMapper;
using BLL.Services.Contracts;
using BLL.Services.Models.DtoModels;
using DAL.Models.Entity;
using DAL.Repositories.Contracts;

namespace BLL.Services.Implementations
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

        public async Task<UserDto> GetUserByLogin(string login)
        {
            var user = await _userRepository.GetUserByLogin(login);
            var userDto = _mapper.Map<UserDto>(user);

            return userDto;
        }

        public async Task<bool> IsExistUser(string login)
        {
            bool isExist = await _userRepository.IsExistUser(login);

            return isExist;
        }

        public async Task UpdateUser(UserDto command)
        {
            var user = _mapper.Map<User>(command);
            await _userRepository.UpdateUser(user);
        }
    }
}