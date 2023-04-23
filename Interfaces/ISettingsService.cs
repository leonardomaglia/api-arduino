using api_arduino.DTOs;
using api_arduino.Models;

namespace api_arduino.Interfaces
{
    public interface ISettingsService
    {
        Task<SettingsDTO> GetSettings(string deviceId);
        Task SaveSettings(string deviceId, SaveSettingsDTO dto);
    }
}