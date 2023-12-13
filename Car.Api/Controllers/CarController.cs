using AutoMapper;
using CarWebService.BLL.Services.Contracts;
using CarWebService.BLL.Services.Models.DtoModels;
using CarWebService.BLL.Services.Models.View;
using CarWebService.DAL.Common.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarWebService.API.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarDto carDto)
        {
            var carId = await _carServices.AddCar(carDto);

            return Created($"{carId}", Url.Action(nameof(GetCarById), new { id = carId }));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCar(CarDto carDto)
        {
            try
            {
                await _carServices.UpdateCar(carDto);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarDto>> GetCarById(int id)
        {
            try
            {
                var car = await _carServices.GetCarById(id);

                return Ok(car);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<ActionResult<List<CarVm>>> GetCars()
        {
            try
            {
                var cars = await _carServices.GetAllCars();

                return Ok(cars);
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _carServices.DeleteCar(id);

                return Ok();
            }
            catch (NotFoundException)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }
    }
}
