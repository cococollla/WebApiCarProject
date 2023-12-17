namespace CarWebService.DAL.Models.Entity
{
    /// <summary>
    /// Модель цвета.
    /// </summary>
    public class Color
    {
        /// <summary>
        /// Идентфикатор цвета.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название цвета.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Ссылка на связанную таблицу автомобилей.
        /// </summary>
        public List<Car> Cars { get; set; }
    }
}
