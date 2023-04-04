using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using api_arduino.Interfaces;
using api_arduino.Models;

namespace api_arduino.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ArduinoDbContext _dbContext;

        public SettingsService(ArduinoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetSettings(string deviceId)
        {
            return "bla";
        }

        public async Task SaveSettings(string deviceId, SaveSettingsDTO dto)
        {
            string[] ports = SerialPort.GetPortNames();

            var device = _dbContext.Devices
                .FirstOrDefault(x => x.Name == deviceId);

            if (device == null)
            {
                device = new Devices
                {
                    Name = deviceId
                };

                _dbContext.Devices.Add(device);
                await _dbContext.SaveChangesAsync();
            }

            var settings = _dbContext.Settings
                .FirstOrDefault(x => x.DeviceId == device.Id);

            if (settings == null)
            {
                settings = new Settings
                {
                    DeviceId = device.Id,
                    HumidityTrigger = dto.HumidityTrigger
                };

                _dbContext.Settings.Add(settings);
            }

            var schedules = _dbContext.Schedules
                .Where(w => w.DeviceId == device.Id)
                .ToList();

            foreach(var schedule in schedules)
            {
                _dbContext.Schedules.Remove(schedule);
            }

            foreach (var schedule in dto.Schedules)
            {
                _dbContext.Schedules.Add(new Schedules
                {
                    DeviceId = device.Id,
                    Time = schedule
                });
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}