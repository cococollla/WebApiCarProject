using CarWebService.BLL.Services.Models.DtoModels;
using CarWebService.BLL.Services.Models.View;

namespace CarWebService.BLL.Services.Contracts
{
    public interface ICarServices
    {
        public Task<int> AddCar(CarDto command);
        public Task<List<CarVm>> GetAllCars();
        public Task<CarVm> GetCarById(int id);
        public Task UpdateCar(CarDto command);
        public Task DeleteCar(int id);
    }
}
