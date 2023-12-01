using CarWebService.BLL.Services.Models.DtoModels;

namespace CarWebService.BLL.Services.Contracts
{
    public interface ICarServices
    {
        public Task<int> AddCar(CarDto command);
        public Task<List<CarDto>> GetAllCars();
        public Task<CarDto> GetCarById(int id);
        public Task UpdateCar(CarDto command);
        public Task DeleteCar(int id);
    }
}
