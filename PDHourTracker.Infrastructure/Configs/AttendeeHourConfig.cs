using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PDHourTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Configs
{
    public class AttendeeHourConfig : IEntityTypeConfiguration<AttendeeHour>
    {
        public void Configure(EntityTypeBuilder<AttendeeHour> builder)
        {
            builder.Property(x => x.AttendeeId)
                .IsRequired();
            builder.Property(x => x.WorkshopId)
                .IsRequired();
            builder.Property(x => x.PDHours)
                .HasColumnType("decimal(4,2)")
                .IsRequired();
            builder.Property(x => x.Comments)
                .HasMaxLength(500);

            // Shared by all entities
            builder.HasKey(x => x.Id);
            builder.Property(x => x.RecUser)
                .HasMaxLength(100);
            builder.Property(x => x.ModUser)
                    .HasMaxLength(100);
            builder.Property(x => x.RecDate)
                    .HasColumnType("smalldatetime");
            builder.Property(x => x.ModDate)
                    .HasColumnType("smalldatetime");
        }
    }
}
