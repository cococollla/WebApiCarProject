namespace CarWebService.DAL.Models.Entity
{
    /// <summary>
    /// Модель автомобиля.
    /// </summary>
    public class Car
    {
        /// <summary>
        /// Идентификатор автомобиля.
        /// </summary>
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
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Id на запись с цветом для данного автомобиля.
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// Ссылка на объект модели цветов для автомобилей.
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Id на запись с брэндом для данного автомобиля.
        /// </summary>
        public int BrandId { get; set; }

        /// <summary>
        /// Ссылка на объект модели брэндов для автомобилей.
        /// </summary>
        public Brand Brand { get; set; }
    }
}
