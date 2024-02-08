using AutoMapper;
using CarWebService.BLL.Models.View;
using CarWebService.DAL.Models.Entity;

namespace CarWebService.BLL.Profiles
{
    public class BrandProfile : Profile
    {
        public BrandProfile()
        {
            CreateMap<Brand, BrandVm>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(brand => brand.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(brand => brand.Name));
        }
    }
}
