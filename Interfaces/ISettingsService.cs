namespace api_arduino.Interfaces
{
    public interface ISettingsService
    {
        Task<string> GetSettings(string deviceId);
    }
}