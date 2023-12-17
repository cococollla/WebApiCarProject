namespace CarWebService.DAL.Models.Entity
{
    /// <summary>
    /// Модель бренда автомобиля.
    /// </summary>
    public class Brand
    {
        /// <summary>
        /// Идентификатор бренда.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название бренда.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на связанную таблицу автомобилей.
        /// </summary>
        public List<Car> Cars { get; set; }
    }
}
