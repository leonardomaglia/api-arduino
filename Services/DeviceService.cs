using api_arduino.Interfaces;
using System.IO.Ports;

namespace api_arduino.Services
{
    public class DeviceService : IDeviceService
    {
        private readonly ArduinoDbContext _dbContext;

        public DeviceService(ArduinoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Connect(string deviceId)
        {
            var device = CreateDevice(deviceId);

            device.Open();
            bool isOpen = device.IsOpen;
            device.Close();

            return isOpen;
        }

        public async Task<int> GetHumidity(string deviceId)
        {
            var device = CreateDevice(deviceId);

            try
            {
                device.Write("read_moisture\n");
                string response = device.ReadLine();
                int moistureLevel = int.Parse(response);

                device.Close();

                return moistureLevel;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao se conectar com o dispositivo. {ex.InnerException}");
            }

            return 0;
        }

        public async Task TriggerHumidity(string deviceId)
        {
            var device = CreateDevice(deviceId);

            try
            {
                device.Write("trigger_water_pump\n");

                device.Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao se conectar com o dispositivo. {ex.InnerException}");
            }
        }

        private SerialPort CreateDevice(string deviceId)
        {
            var device = new SerialPort(deviceId, 9600);

            if (device.IsOpen is false)
                device.Open();

            return device;
        }
    }
}