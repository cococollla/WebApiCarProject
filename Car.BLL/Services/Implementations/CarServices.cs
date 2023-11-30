using AutoMapper;
using BLL.Services.Contracts;
using BLL.Services.Models.DtoModels;
using DAL.Models.Entity;
using DAL.Repositories.Contracts;

namespace BLL.Services.Implementations
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

        public async Task AddCar(CarDto command)
        {
            var car = _mapper.Map<Car>(command);
            await _carRepository.AddCar(car);
        }

        public async Task DeleteCar(int id)
        {
            await _carRepository.DeleteCar(id);
        }

        public async Task<List<CarDto>> GetAllCars()
        {
            var cars = await _carRepository.GetAllCars();
            var carsDto = _mapper.Map<List<CarDto>>(cars);

            return carsDto;
        }

        public async Task<CarDto> GetCarById(int id)
        {
            var car = await _carRepository.GetCarById(id);
            var carDto = _mapper.Map<CarDto>(car);

            return carDto;
        }

        public async Task UpdateCar(CarDto command)
        {
            var car = _mapper.Map<Car>(command);
            await _carRepository.UpdateCar(car);
        }
    }
}
