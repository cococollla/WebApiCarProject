using AutoMapper;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Models.View;
using CarWebService.DAL.Models.Entity;

namespace CarWebService.BLL.Profiles
{
    public class CarProfile : Profile
    {
        public CarProfile()
        {
            CreateMap<CarDto, Car>()
                .ForMember(dest => dest.BrandId, opt => opt.MapFrom(carDto => carDto.BrandId))
                .ForMember(dest => dest.ColorId, opt => opt.MapFrom(carDto => carDto.ColorId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(carDto => carDto.Price))
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(carDto => carDto.YearRelese))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(carDto => carDto.ShorDescription)).ReverseMap();

            CreateMap<Car, CarVm>()
                .ForMember(dest => dest.YearRelese, opt => opt.MapFrom(car => car.YearRelese))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(car => car.Price))
                .ForMember(dest => dest.ColorName, opt => opt.MapFrom(car => car.Color.Name))
                .ForMember(dest => dest.BrandName, opt => opt.MapFrom(car => car.Brand.Name))
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(car => car.ShorDescription));
        }
    }
}
