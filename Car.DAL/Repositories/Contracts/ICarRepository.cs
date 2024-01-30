using CarWebService.DAL.Models.Entity;

namespace CarWebService.DAL.Repositories.Contracts
{
    public interface ICarRepository
    {
        /// <summary>
        /// Добавляет автомобиль в БД.
        /// </summary>
        /// <param name="request">Данные автомобиля.</param>
        public Task<int> AddCar(Car request);

        /// <summary>
        /// Получает список всех автомобилей в БД.
        /// </summary>
        /// <returns>Список автомобилей.</returns>
        public Task<List<Car>> GetAllCars();

        /// <summary>
        /// Получает запись об автомобиле из БД.
        /// </summary>
        /// <param name="id">Идентификатор по которому будет найден автомобиль.</param>
        /// <returns>Данные автомобиля.</returns>
        public Task<Car> GetCarById(int id);

        /// <summary>
        /// Обновляет данные автомобиля в БД.
        /// </summary>
        /// <param name="request">Обновленные данных.</param>
        public Task<bool> UpdateCar(Car request);

        /// <summary>
        /// Удаляет запись об автомобиле в БД.
        /// </summary>
        /// <param name="id">Идентификатор по которому будет найден автомобиль.</param>
        public Task<bool> DeleteCar(int id);

        /// <summary>
        /// Получает список брендов автомобилей.
        /// </summary>
        /// <returns>Список брендов.</returns>
        public Task<List<Brand>> GetBrands();

        /// <summary>
        /// Получает список цветов для автомобилей.
        /// </summary>
        /// <returns>Список цветов.</returns>
        public Task<List<Color>> GetColors();

        /// <summary>
        /// Получает заданное количество записей для страницы.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Количество записей.</param>
        /// <returns>Список автомобилей.</returns>
        public Task<List<Car>> GetByPage(int page, int pageSize);
    }
}