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

        [HttpPost("connect")]
        public async Task<IActionResult> Connect([FromQuery] string deviceId)
        {
            var result = await _deviceService.Connect(deviceId);

            return base.Ok(result);
        }

        [HttpGet("humidity")]
        public async Task<IActionResult> GetHumidity([FromQuery] string deviceId)
        {
            var result = await _deviceService.GetHumidity(deviceId);

            return base.Ok(result);
        }

        [HttpGet("humidity/trigger")]
        public async Task<IActionResult> TriggerHumidity([FromQuery] string deviceId)
        {
            await _deviceService.TriggerHumidityManually(deviceId);
            return base.Ok();
        }
    }
}