using AutoMapper;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Models.View;
using CarWebService.BLL.Services.Contracts;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;

namespace CarWebService.BLL.Services.Implementations
{
    /// <summary>
    /// Бизнес логика для автомобиля.
    /// </summary>
    public class CarServices : ICarServices
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public CarServices(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }

        /// <summary>
        /// Добавление записи в БД.
        /// </summary>
        /// <param name="command">Данные автомобиля.</param>
        public async Task<int> AddCar(CarDto command)
        {
            var car = _mapper.Map<Car>(command);
            var carId = await _carRepository.AddCar(car);

            return carId;
        }

        /// <summary>
        /// Удаление автомобиля.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля.</param>
        public async Task<bool> DeleteCar(int id)
        {
            var result = await _carRepository.DeleteCar(id);

            return result;
        }

        /// <summary>
        /// Получение списка всех автомобилей.
        /// </summary>
        public async Task<List<CarVm>> GetAllCars()
        {
            var cars = await _carRepository.GetAllCars();
            var carsDto = _mapper.Map<List<CarVm>>(cars);

            return carsDto;
        }

        /// <summary>
        /// Получение автомобиля.
        /// </summary>
        /// <param name="id">Идентификатор автомбиля.</param>
        public async Task<CarVm> GetCarById(int id)
        {
            var car = await _carRepository.GetCarById(id);
            var carDto = _mapper.Map<CarVm>(car);

            return carDto;
        }

        /// <summary>
        /// Обновление записи.
        /// </summary>
        /// <param name="command">Данные для обновления.</param>
        public async Task<bool> UpdateCar(CarDto command)
        {
            var car = _mapper.Map<Car>(command);
            var result = await _carRepository.UpdateCar(car);

            return result;
        }


        /// <summary>
        /// Получает заданное количество записей для страницы.
        /// </summary>
        public async Task<List<CarVm>> GetByPage(int page, int pageSize)
        {
            var cars = await _carRepository.GetByPage(page, pageSize);
            var carsDto = _mapper.Map<List<CarVm>>(cars);

            return carsDto;
        }

        /// <summary>
        /// Получает список брендов автомобилей.
        /// </summary>
        /// <returns>Список брендов.</returns>
        public async Task<List<BrandVm>> GetBrands()
        {
            var brands = await _carRepository.GetBrands();
            var brandsVm = _mapper.Map<List<BrandVm>>(brands);

            return brandsVm;
        }

        /// <summary>
        /// Получает список цветов для автомобилей.
        /// </summary>
        /// <returns>Список цветов.</returns>
        public async Task<List<ColorVm>> GetColors()
        {
            var colors = await _carRepository.GetColors();
            var colorsVm = _mapper.Map<List<ColorVm>>(colors);
            return colorsVm;
        }
    }
}