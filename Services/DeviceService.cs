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

        public async Task<int> GetHumidity(string deviceId)
        {
            return 350;

            // Remover return após testes
            var device = CreateDevice(deviceId);

            if (device.IsOpen is false)
            {
                try
                {
                    device.Open();
                    device.Write("");
                    device.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao se conectar com o dispositivo. {ex.InnerException}");
                }
            }
        }

        public async Task TriggerHumidity(string deviceId)
        {
            var device = CreateDevice(deviceId);

            if (device.IsOpen is false)
            {
                try
                {
                    device.Open();
                    device.Write("");
                    device.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Erro ao se conectar com o dispositivo. {ex.InnerException}");
                }
            }
        }

        private SerialPort CreateDevice(string deviceId)
        {
            return new SerialPort
            {
                PortName = deviceId,
                BaudRate = 9600
            };
        }
    }
}