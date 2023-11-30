namespace BLL.Services.Models.DtoModels
{
    public class CarDto
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public int ColorId { get; set; }
        public string YearRelese { get; set; }
        public double Price { get; set; }
        public string? ShorDescription { get; set; }
    }
}
