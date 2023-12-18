namespace CarWebService.BLL.Models.DtoModels
{
    /// <summary>
    /// Автомобиль.
    /// </summary>
    public class CarDto
    {
        /// <summary>
        /// Идентификатор автомоблия
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата выпуска.
        /// </summary>
        public string YearRelese { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Коротокое описание.
        /// </summary>
        public string? ShortDescription { get; set; }

        /// <summary>
        /// Идентификатор цвета.
        /// </summary>
        public int ColorId { get; set; }

        /// <summary>
        /// Идентификатор бренда.
        /// </summary>
        public int BrandId { get; set; }
    }
}
