using api_arduino.DTOs;
using api_arduino.Interfaces;
using api_arduino.Models;
using Hangfire;
using Hangfire.Storage;

namespace api_arduino.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ArduinoDbContext _dbContext;
        private readonly IDeviceService _deviceService;

        public SettingsService(ArduinoDbContext dbContext, IDeviceService deviceService)
        {
            _dbContext = dbContext;
            _deviceService = deviceService;
        }

        public async Task<SettingsDTO> GetSettings(string deviceId)
        {
            var device = _dbContext.Devices
                .FirstOrDefault(x => x.Name == deviceId);

            if (device == null)
                return null;

            var settings = _dbContext.Settings
                .FirstOrDefault(x => x.DeviceId == device.Id);

            var schedules = _dbContext.Schedules
                .Where(w => w.DeviceId == device.Id)
                .ToList();

            if (settings != null)
            {
                return new SettingsDTO
                {
                    HumidityTrigger = settings.HumidityTrigger,
                    Schedules = schedules.ConvertAll(x => x.Time)
                };
            }

            return null;
        }

        public async Task SaveSettings(string deviceId, SaveSettingsDTO dto)
        {
            RemoveAllRecurringTasks();

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

            RecurringJob.AddOrUpdate(() => _deviceService.TriggerWaterPump(deviceId, dto.HumidityTrigger), Cron.MinuteInterval(5));

            var schedules = _dbContext.Schedules
                .Where(w => w.DeviceId == device.Id)
                .ToList();

            foreach(var schedule in schedules)
            {
                _dbContext.Schedules.Remove(schedule);
            }

            foreach (var schedule in dto.Schedules)
            {
                var time = schedule.Split(":");
                var hour = Convert.ToInt32(time[0]);
                var minute = Convert.ToInt32(time[^1]);

                RecurringJob.AddOrUpdate(() => _deviceService.TriggerWaterPumpManually(deviceId), Cron.Daily(hour, minute), TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"));

                _dbContext.Schedules.Add(new Schedules
                {
                    DeviceId = device.Id,
                    Time = schedule
                });
            }

            await _dbContext.SaveChangesAsync();
        }

        private void RemoveAllRecurringTasks()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
        }
    }
}