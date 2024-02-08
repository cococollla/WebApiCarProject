using AutoMapper;
using CarWebService.BLL.Models.View;
using CarWebService.DAL.Models.Entity;

namespace CarWebService.BLL.Profiles
{
    public class ColorProfile : Profile
    {
        public ColorProfile()
        {
            CreateMap<Color, ColorVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(color => color.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(color => color.Name));
        }
    }
}
