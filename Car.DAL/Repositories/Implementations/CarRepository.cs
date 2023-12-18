using CarWebService.DAL.Common.Exceptions;
using CarWebService.DAL.Models.Entity;
using CarWebService.DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CarWebService.DAL.Repositories.Implementations
{
    /// <summary>
    /// Репозиторий для управления записями автомоблей в БД.
    /// </summary>
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationContext _context;

        public CarRepository(ApplicationContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Добавляет автомобиль в БД.
        /// </summary>
        /// <param name="request">Данные автомобиля.</param>
        public async Task<int> AddCar(Car request)
        {
            await _context.Cars.AddAsync(request);
            await _context.SaveChangesAsync();

            return request.Id;
        }

        /// <summary>
        /// Удаляет запись об автомобиле в БД.
        /// </summary>
        /// <param name="id">Идентификатор по которому будет найден автомобиль.</param>
        public async Task DeleteCar(int id)
        {
            var car = await _context.Cars.FindAsync(id);

            if (car == null)
            {
                throw new NotFoundException($"Car is not found");
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает список всех автомобилей в БД.
        /// </summary>
        /// <returns>Список автомобилей.</returns>
        public async Task<List<Car>> GetAllCars()
        {
            var cars = await _context.Cars.Include(b => b.Brand).Include(c => c.Color).ToListAsync();

            if (cars == null)
            {
                throw new NotFoundException("Not found");
            }

            return cars;
        }

        /// <summary>
        /// Получает запись об автомобиле из БД.
        /// </summary>
        /// <param name="id">Идентификатор по которому будет найден автомобиль.</param>
        /// <returns>Данные автомобиля.</returns>
        public async Task<Car> GetCarById(int id)
        {
            var car = await _context.Cars.Include(b => b.Brand).Include(c => c.Color).FirstOrDefaultAsync(car => car.Id == id);

            if (car == null)
            {
                throw new NotFoundException($"Car is not found");
            }

            return car;
        }

        /// <summary>
        /// Обновляет данные автомобиля в БД.
        /// </summary>
        /// <param name="request">Обновленные данных.</param>
        public async Task UpdateCar(Car request)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == request.Id);

            if (car == null)
            {
                throw new NotFoundException($"Car is not found");
            }

            car.YearRelese = request.YearRelese;
            car.Price = request.Price;
            car.ShortDescription = request.ShortDescription;
            car.BrandId = request.BrandId;
            car.ColorId = request.ColorId;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает список брендов автомобилей.
        /// </summary>
        /// <returns>Список брендов.</returns>
        public async Task<List<Brand>> GetBrands()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();

            return brands;
        }

        /// <summary>
        /// Получает список цветов для автомобилей.
        /// </summary>
        /// <returns>Список цветов.</returns>
        public async Task<List<Color>> GetColors()
        {
            List<Color> colors = await _context.Colors.ToListAsync();

            return colors;
        }
    }
}
