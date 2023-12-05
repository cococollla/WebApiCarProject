using CarWebService.BLL.Services.Contracts;
using CarWebService.BLL.Services.Models.DtoModels;
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

        public CarController(ICarServices carServices)
        {
            _carServices = carServices;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateCar(CarDto carDto)
        {
            try
            {
                var carId = await _carServices.AddCar(carDto);
                //ResponseHeaderHelper.AddToResponseHeader(HttpContext, carId);

                return Created($"api/Car/GetCarById/{carId}", carId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, ex.Message);
            }
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("id")]
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetCars()
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("id")]
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
