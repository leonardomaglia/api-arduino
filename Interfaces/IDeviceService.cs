namespace api_arduino.Interfaces
{
    public interface IDeviceService
    {
        Task<bool> Connect(string deviceId);
        Task<int> GetHumidity(string deviceId);
        Task TriggerHumidity(string deviceId, int humidityTrigger);
        Task TriggerHumidityManually(string deviceId);
    }
}