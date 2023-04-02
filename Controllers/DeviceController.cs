using api_arduino.Interfaces;
using api_arduino.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_arduino.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }

        [HttpGet("humidity/{deviceId}")]
        public async Task<IActionResult> GetHumidity([FromRoute] string deviceId)
        {
            var result = await _deviceService.GetHumidity(deviceId);

            return base.Ok(result);
        }
    }
}