namespace CarWebService.DAL.Models.Entity
{
    public class Car
    {
        public int Id { get; set; }

        /// <summary>
        /// год выпуска автомобиля.
        /// </summary>
        public string YearRelese { get; set; }

        /// <summary>
        /// Цена автомобиля.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Короткое описание автомобиля.
        /// </summary>
        public string? ShorDescription { get; set; }

        /// <summary>
        /// Id на запись с цветом для данного автомобиля.
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// Ссылка на объект модели цветов для автомобилей
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Id на запись с брэндом для данного автомобиля
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Ссылка на объект модели брэндов для автомобилей
        /// </summary>
        public Brand Brand { get; set; }
    }
}
