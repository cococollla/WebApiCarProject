namespace CarWebService.BLL.Models.DtoModels
{
    public class CarDto
    {
        public int Id { get; set; }

        public string YearRelese { get; set; }

        public double Price { get; set; }

        public string? ShortDescription { get; set; }

        public int ColorId { get; set; }

        public int BrandId { get; set; }
    }
}
