using api_arduino.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_arduino.Maps
{
    public class SchedulesMap : IEntityTypeConfiguration<Schedules>
    {
        public void Configure(EntityTypeBuilder<Schedules> builder)
        {
            // Columns
            builder.ToTable("Schedules");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.DeviceId)
                .IsRequired();

            builder.Property(x => x.Time)
                .IsRequired();
        }
    }
}