using AutoMapper;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Models.View;
using CarWebService.BLL.Services.Contracts;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;

namespace CarWebService.BLL.Services.Implementations
{
    public class CarServices : ICarServices
    {
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;

        public CarServices(IMapper mapper, ICarRepository carRepository)
        {
            _mapper = mapper;
            _carRepository = carRepository;
        }

        public async Task<int> AddCar(CarDto command)
        {
            var car = _mapper.Map<Car>(command);
            var carId = await _carRepository.AddCar(car);

            return carId;
        }

        public async Task DeleteCar(int id)
        {
            await _carRepository.DeleteCar(id);
        }

        public async Task<List<CarVm>> GetAllCars()
        {
            var cars = await _carRepository.GetAllCars();
            var carsDto = _mapper.Map<List<CarVm>>(cars);

            return carsDto;
        }

        public async Task<CarVm> GetCarById(int id)
        {
            var car = await _carRepository.GetCarById(id);
            var carDto = _mapper.Map<CarVm>(car);

            return carDto;
        }

        public async Task UpdateCar(CarDto command)
        {
            var car = _mapper.Map<Car>(command);
            await _carRepository.UpdateCar(car);
        }
    }
}
