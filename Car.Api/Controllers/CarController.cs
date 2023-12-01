using BLL.Services.Contracts;
using BLL.Services.Models.DtoModels;
using DAL.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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

        [HttpPost]
        public async Task<IActionResult> CreateCar(CarDto carDto)
        {
            await _carServices.AddCar(carDto);

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Updatecar(CarDto carDto)
        {
            await _carServices.UpdateCar(carDto);

            return Ok();
        }

        [HttpGet("id")]
        public async Task<ActionResult<CarDto>> GetCarById(int id)
        {
            try
            {
                var car = await _carServices.GetCarById(id);

                return Ok(car);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<CarDto>>> GetCars()
        {
            var cars = await _carServices.GetAllCars();

            return Ok(cars);
        }

        [HttpDelete("id")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            try
            {
                await _carServices.DeleteCar(id);

                return Ok();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
