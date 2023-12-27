using AutoMapper;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.DAL.Models.Entity;

namespace CarWebService.BLL.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(userDto => userDto.Id))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(userDto => userDto.Name))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(userDto => userDto.Email))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(userDto => userDto.Password)).ReverseMap();
        }
    }
}