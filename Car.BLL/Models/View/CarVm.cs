namespace CarWebService.BLL.Models.View
{
    /// <summary>
    /// Модель для отображения данных автомобиля.
    /// </summary>
    public class CarVm
    {
        public int Id { get; set; }
        /// <summary>
        /// Название цвета.
        /// </summary>
        public string ColorName { get; set; }

        /// <summary>
        /// Название бренда.
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// Дата выпуска.
        /// </summary>
        public string YearRelese { get; set; }

        /// <summary>
        /// Цена.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Краткое описание.
        /// </summary>
        public string? ShortDescription { get; set; }

    }
}