using api_arduino.Interfaces;

namespace api_arduino.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ArduinoDbContext _dbContext;

        public DeviceService(ArduinoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetHumidity(string deviceId)
        {
            return 350;
        }
    }
}