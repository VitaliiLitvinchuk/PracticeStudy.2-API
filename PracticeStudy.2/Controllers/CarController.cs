using Core.Interfaces.Services.Machine;
using Core.Pagination;
using Core.ViewModels.Machine;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Middleware.Handler;

namespace PracticeStudy._2.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    [ProducesResponseType(typeof(BadRequestResponse), StatusCodes.Status400BadRequest)]
    public class CarController(ICarService carService) : ControllerBase
    {
        private readonly ICarService _carService = carService;

        private OkObjectResult OkData(object? data)
        {
            return Ok(new { data });
        }

        #region Get
        [HttpPost("get-characteristics")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCharacteristics([FromBody] Guid carId)
        {
            return OkData(await _carService.GetCharacteristics(carId));
        }

        [HttpPost("get-cars-by-user-id")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCarsByUserId([FromBody] Guid userId)
        {
            return OkData(await _carService.GetCarsByUserId(userId));
        }

        [HttpPost("get-cars")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCars([FromBody] CarPagination pagination)
        {
            return OkData(await _carService.GetCars(pagination));
        }

        [HttpPost("get-years")]
        [AllowAnonymous]
        public async Task<IActionResult> GetYears()
        {
            return OkData(await _carService.GetYears());
        }

        [HttpPost("get-brands")]
        [AllowAnonymous]
        public async Task<IActionResult> GetBrands()
        {
            return OkData(await _carService.GetBrands());
        }

        [HttpPost("get-photos")]
        [AllowAnonymous]
        public async Task<IActionResult> GetPhotos([FromBody] Guid carId)
        {
            return OkData(await _carService.GetPhotos(carId));
        }

        [HttpPost("get-properties")]
        [AllowAnonymous]
        public async Task<IActionResult> GetProperties()
        {
            return OkData(await _carService.GetProperties());
        }
        #endregion

        #region Post
        [HttpPost("add-characteristic")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCharacteristic([FromBody] CharacteristicViewModel characteristic)
        {
            return OkData(await _carService.AddCharacteristic(characteristic));
        }

        [HttpPost("add-brand")]
        [AllowAnonymous]
        public async Task<IActionResult> AddBrand([FromBody] CarBrandViewModel brand)
        {
            return OkData(await _carService.AddBrand(brand));
        }

        [HttpPost("add-car")]
        [AllowAnonymous]
        public async Task<IActionResult> AddCar([FromBody] CarViewModel car)
        {
            return OkData(await _carService.AddCar(car));
        }

        [HttpPost("add-photo")]
        [AllowAnonymous]
        public async Task<IActionResult> AddPhoto([FromBody] CarPhotoViewModel photo)
        {
            return OkData(await _carService.AddPhoto(photo));
        }

        [HttpPost("add-year")]
        [AllowAnonymous]
        public async Task<IActionResult> AddYear([FromBody] CarYearViewModel year)
        {
            return OkData(await _carService.AddYear(year));
        }

        [HttpPost("add-property")]
        [AllowAnonymous]
        public async Task<IActionResult> AddProperty([FromBody] PropertyViewModel property)
        {
            return OkData(await _carService.AddProperty(property));
        }
        #endregion

        #region Delete
        [HttpPost("delete-characteristic")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCharacteristic([FromBody] Guid id)
        {
            await _carService.DeleteCharacteristic(id);
            return OkData("Deleted");
        }

        [HttpPost("delete-brand")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteBrand([FromBody] string name)
        {
            await _carService.DeleteBrand(name);
            return Ok("Deleted");
        }

        [HttpPost("delete-car")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteCar([FromBody] Guid id)
        {
            await _carService.DeleteCar(id);
            return Ok("Deleted");
        }

        [HttpPost("delete-photo")]
        [AllowAnonymous]
        public async Task<IActionResult> DeletePhoto([FromBody] string name)
        {
            await _carService.DeletePhoto(name);
            return Ok("Deleted");
        }

        [HttpPost("delete-year")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteYear([FromBody] int year)
        {
            await _carService.DeleteYear(year);
            return Ok("Deleted");
        }

        [HttpPost("delete-property")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteProperty([FromBody] string name)
        {
            await _carService.DeleteProperty(name);
            return Ok("Deleted");
        }
        #endregion

        #region Put
        [HttpPost("update-characteristic")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCharacteristic([FromBody] CharacteristicViewModel characteristic, Guid id)
        {
            await _carService.UpdateCharacteristic(characteristic, id);
            return OkData("Updated");
        }

        [HttpPost("update-car")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateCar([FromBody] CarViewModel car, Guid id)
        {
            await _carService.UpdateCar(car, id);
            return Ok("Updated");
        }

        [HttpPost("update-property")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateProperty([FromBody] PropertyViewModel property)
        {
            await _carService.UpdateProperty(property, property.Name);
            return Ok("Updated");
        }

        #endregion
    }
}
