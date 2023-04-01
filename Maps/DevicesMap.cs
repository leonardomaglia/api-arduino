using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api_arduino.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace api_arduino.Maps
{
    public class DevicesMap : IEntityTypeConfiguration<Devices>
    {
        public void Configure(EntityTypeBuilder<Devices> builder)
        {
            // Columns
            builder.ToTable("Devices");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired();

            // builder.Property(x => x.Port)
            //     .IsRequired();
        }
    }
}