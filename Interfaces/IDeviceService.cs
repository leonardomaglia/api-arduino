namespace api_arduino.Interfaces
{
    public interface IDeviceService
    {
        Task<bool> Connect(string deviceId);
        Task<int> GetHumidity(string deviceId);
        Task TriggerWaterPump(string deviceId, int humidityTrigger);
        Task TriggerWaterPumpManually(string deviceId);
    }
}