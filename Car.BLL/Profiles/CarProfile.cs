﻿using AutoMapper;
using CarWebService.BLL.Services.Models.DtoModels;
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
                .ForMember(dest => dest.ShorDescription, opt => opt.MapFrom(carDto => carDto.ShorDescription));
        }
    }
}
