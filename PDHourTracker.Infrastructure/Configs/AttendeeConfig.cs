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
    public class AttendeeConfig : IEntityTypeConfiguration<Attendee>
    {

        public void Configure(EntityTypeBuilder<Attendee> builder)
        {
            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired();
            builder.Property(x => x.MiddleName)
                .HasMaxLength(100);
            builder.Property(x => x.JobTitle)
                .HasMaxLength(100);
            builder.Property(x => x.AgencyId)
                .IsRequired();
            builder.Property(x => x.CertId)
                .HasMaxLength(20)
                .IsRequired();

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
