﻿namespace CarWebService.BLL.Services.Models.DtoModels
{
    public class CarDto
    {
        public string YearRelese { get; set; }
        public double Price { get; set; }
        public string? ShorDescription { get; set; }

        public int ColorId { get; set; }
        public int BrandId { get; set; }
    }
}
