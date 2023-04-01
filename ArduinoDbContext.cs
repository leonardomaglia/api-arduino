using api_arduino.Maps;
using api_arduino.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace api_arduino
{
    public class ArduinoDbContext : DbContext
    {
        public ArduinoDbContext(DbContextOptions<ArduinoDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Devices> Devices { get; set; }
        public virtual DbSet<Schedules> Schedules { get; set; }
        public virtual DbSet<Settings> Settings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new DevicesMap());
            modelBuilder.ApplyConfiguration(new SchedulesMap());
            modelBuilder.ApplyConfiguration(new SettingsMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.ConfigureWarnings(warnings =>
                warnings.Default(WarningBehavior.Ignore).Log(CoreEventId.DetachedLazyLoadingWarning));
        }
    }
}