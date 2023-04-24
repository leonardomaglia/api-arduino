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

                device.Close();

                return int.Parse(humidity);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao se conectar com o dispositivo. {ex.InnerException}");
            }
        }

        public async Task TriggerHumidity(string deviceId, int humidityTrigger)
        {
            try
            {
                var humidity = await GetHumidity(deviceId);

                if (humidity <= humidityTrigger)
                {
                    var device = CreateDevice(deviceId);

                    device.Write("trigger_pump\n");

                    device.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao se conectar com o dispositivo. {ex.InnerException}");
            }
        }

        public async Task TriggerHumidityManually(string deviceId)
        {
            try
            {
                var device = CreateDevice(deviceId);

                device.Write("trigger_pump\n");

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