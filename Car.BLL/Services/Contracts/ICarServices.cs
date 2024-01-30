using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Models.View;

namespace CarWebService.BLL.Services.Contracts
{
    public interface ICarServices
    {
        /// <summary>
        /// Добавление записи в БД.
        /// </summary>
        /// <param name="command">Данные автомобиля.</param>
        public Task<int> AddCar(CarDto command);

        /// <summary>
        /// Получение списка всех автомобилей.
        /// </summary>
        public Task<List<CarVm>> GetAllCars();

        /// <summary>
        /// Получение автомобиля.
        /// </summary>
        /// <param name="id">Идентификатор автомбиля.</param>
        public Task<CarVm> GetCarById(int id);

        /// <summary>
        /// Обновление записи.
        /// </summary>
        /// <param name="command">Данные для обновления.</param>
        public Task<bool> UpdateCar(CarDto command);

        /// <summary>
        /// Удаление автомобиля.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля.</param>
        public Task<bool> DeleteCar(int id);

        /// <summary>
        /// Получает заданное количество записей для страницы.
        /// </summary>
        public Task<List<CarVm>> GetByPage(int page, int pageSize);
    }
}