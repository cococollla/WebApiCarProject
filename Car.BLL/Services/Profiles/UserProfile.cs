using AutoMapper;
using BLL.Services.Models.DtoModels;
using DAL.Models.Entity;

namespace BLL.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(dest => dest.Login, opt => opt.MapFrom(userDto => userDto.Login))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userDto => userDto.Password))
                .ForMember(dest => dest.Role.Name, opt => opt.MapFrom(userDto => userDto.RoleName));
        }
    }
}
