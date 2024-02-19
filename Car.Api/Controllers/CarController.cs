using AutoMapper;
using CarWebService.API.Models;
using CarWebService.BLL.Models.DtoModels;
using CarWebService.BLL.Models.View;
using CarWebService.BLL.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWebService.API.Controllers
{
    /// <summary>
    /// Контроллер для управления и просмотра автомобилей.
    /// </summary>
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CarController : ControllerBase
    {
        private readonly ICarServices _carServices;
        private readonly IMapper _mapper;

        public CarController(ICarServices carServices, IMapper mapper)
        {
            _carServices = carServices;
            _mapper = mapper;
        }

        /// <summary>
        /// Создание записи об автомобиле в БД.
        /// </summary>
        /// <param name="carDto">Данные автомобиля.</param>
        [HttpPost]
        [Authorize(Roles = "Manager")]
        public async Task<IActionResult> CreateCar(CarDto carDto)
        {
            var carId = await _carServices.AddCar(carDto);

            return Created($"{carId}", Url.Action(nameof(GetCarById), new { id = carId }));
        }

        /// <summary>
        /// Обновление данных автомобиля в БД.
        /// </summary>
        /// <param name="carDto">Данные для обновления.</param>
        [HttpPut]
        public async Task<IActionResult> UpdateCar(CarDto carDto)
        {
            var result = await _carServices.UpdateCar(carDto);

            if (!result)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return NoContent();
        }

        /// <summary>
        /// Поиск записи автомобиля по его идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор автомобиля для поиска.</param>
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCarById(int id)
        {
            var car = await _carServices.GetCarById(id);

            if (car == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(car);
        }

        /// <summary>
        /// Вывод записей всех автомобилей.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<CarVm>>> GetCars()
        {
            var cars = await _carServices.GetAllCars();

            if (cars == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(cars);
        }

        /// <summary>
        /// Удаление автомобился из БД.
        /// </summary>
        /// <param name="id">Id автомобиля для удаления.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var result = await _carServices.DeleteCar(id);

            if (!result)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok();
        }


        /// <summary>
        /// Получает заданное количество записей для страницы.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="pageSize">Количество записей.</param>
        /// <returns>Список автомобилей.</returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCarsByPage(int page, int pageSize)
        {
            var carsByPage = await _carServices.GetByPage(page, pageSize);
            var cars = await _carServices.GetAllCars();

            if (carsByPage == null || cars == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            var response = new PaginateResponse
            {
                Cars = carsByPage,
                TotalItems = cars.Count()
            };

            return Ok(response);
        }


        /// <summary>
        /// Получает список цветов для автомобилей.
        /// </summary>
        /// <returns>Список цветов.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ColorVm>>> GetColors()
        {
            var colors = await _carServices.GetColors();

            if (colors == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(colors);
        }

        /// <summary>
        /// Получает список брендов автомобилей.
        /// </summary>
        /// <returns>Список брендов.</returns>
        [HttpGet]
        public async Task<ActionResult<List<BrandVm>>> GetBrands()
        {
            var brands = await _carServices.GetBrands();

            if (brands == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(brands);
        }
    }
}