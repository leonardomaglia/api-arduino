using api_arduino.Interfaces;
using api_arduino.Models;
using Microsoft.AspNetCore.Mvc;

namespace api_arduino.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] string deviceId)
        {
            string result = await _settingsService.GetSettings(deviceId);

            return base.Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Save([FromQuery] string deviceId, [FromBody] SaveSettingsDTO dto)
        {
            await _settingsService.SaveSettings(deviceId, dto);
            return Ok();
        }
    }
}