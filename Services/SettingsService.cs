using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_arduino.Interfaces;

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
    }
}