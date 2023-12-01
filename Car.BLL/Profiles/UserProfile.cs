using AutoMapper;
using CarWebService.BLL.Services.Models.DtoModels;
using CarWebService.DAL.Models.Entity;

namespace CarWebService.BLL.Profiles
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
                .ForMember(dest => dest.RoleId, opt => opt.MapFrom(userDto => userDto.RoleId));
        }
    }
}
