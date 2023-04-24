using api_arduino.Interfaces;
using System.IO.Ports;

namespace api_arduino.Services
{
    public class DeviceService : IDeviceService
    {
        public async Task<bool> Connect(string deviceId)
        {
            var device = CreateDevice(deviceId);
            var isOpen = device.IsOpen;

            device.Close();

            return isOpen;
        }

        public async Task<int> GetHumidity(string deviceId)
        {
            try
            {
                var device = CreateDevice(deviceId);

                device.Write("read_humidity\n");
                string humidity = device.ReadLine();

                return int.Parse(humidity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao se conectar com o dispositivo. {ex.InnerException}");
            }
        }

        public async Task TriggerHumidity(string deviceId)
        {
            try
            {
                var device = CreateDevice(deviceId);

                device.Write("trigger_water_pump\n");
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