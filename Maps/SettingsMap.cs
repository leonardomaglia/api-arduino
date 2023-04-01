using api_arduino.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_arduino.Maps
{
    public class SettingsMap : IEntityTypeConfiguration<Settings>
    {
        public void Configure(EntityTypeBuilder<Settings> builder)
        {
            // Columns
            builder.ToTable("Settings");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DeviceId)
                .IsRequired();

            builder.Property(x => x.HumidityTrigger)
                .IsRequired();
        }
    }
}