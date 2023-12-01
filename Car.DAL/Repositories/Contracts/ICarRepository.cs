using CarWebService.DAL.Models.Entity;

namespace CarWebService.DAL.Repositories.Contracts
{
    public interface ICarRepository
    {
        public Task<int> AddCar(Car request);
        public Task<List<Car>> GetAllCars();
        public Task<Car> GetCarById(int id);
        public Task UpdateCar(Car request);
        public Task DeleteCar(int id);
        public Task<List<Brand>> GetBrands();
        public Task<List<Color>> GetColors();
    }
}
