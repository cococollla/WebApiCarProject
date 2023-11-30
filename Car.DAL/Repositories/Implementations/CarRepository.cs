using AutoMapper;
using DAL.Common.Exceptions;
using DAL.Models.Entity;
using DAL.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationContext _context;

        public CarRepository(ApplicationContext context, IMapper mapper)
        {
            _context = context;
        }


        /// <summary>
        /// Добавляет автомобиль в БД
        /// </summary>
        /// <param name="request">Данные автомобиля</param>
        public async Task AddCar(Car request)
        {
            await _context.Cars.AddAsync(request);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удаляет запись об автомобиле в БД
        /// </summary>
        /// <param name="id">Id по которому будет найден автомобиль</param>
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
        /// Получает список всех автомобилей в БД
        /// </summary>
        /// <returns>Список автомобилей</returns>
        public async Task<List<Car>> GetAllCars()
        {

            var cars = await _context.Cars.Include(b => b.Brand).Include(c => c.Color).ToListAsync();

            return cars;
        }

        /// <summary>
        /// Получает запись об автомобиле из БД
        /// </summary>
        /// <param name="id">Id по которому будет найден автомобиль</param>
        /// <returns>Данные автомобиля</returns>
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
        /// Обновляет данные автомобиля в БД
        /// </summary>
        /// <param name="request">Обновленные данные</param>
        public async Task UpdateCar(Car request)
        {
            var car = await _context.Cars.FirstOrDefaultAsync(car => car.Id == request.Id);

            if (car == null)
            {
                throw new NotFoundException($"Car is not found");
            }

            car.YearRelese = request.YearRelese;
            car.Price = request.Price;
            car.ShorDescription = request.ShorDescription;
            car.BrandId = request.BrandId;
            car.ColorId = request.ColorId;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает список брэндов автомобилей
        /// </summary>
        /// <returns>Список брэндов</returns>
        public async Task<List<Brand>> GetBrands()
        {
            List<Brand> brands = await _context.Brands.ToListAsync();

            return brands;
        }

        /// <summary>
        /// Получает список цветов для автомобилей
        /// </summary>
        /// <returns>Список цветов</returns>
        public async Task<List<Color>> GetColors()
        {
            List<Color> colors = await _context.Colors.ToListAsync();

            return colors;
        }
    }
}
