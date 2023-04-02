namespace api_arduino.Interfaces
{
    public interface IDeviceService
    {
        Task<int> GetHumidity(string deviceId);
    }
}