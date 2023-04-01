using api_arduino.Models;

namespace api_arduino.Interfaces
{
    public interface ISettingsService
    {
        Task<string> GetSettings(string deviceId);
        Task SaveSettings(string deviceId, SaveSettingsDTO dto);
    }
}