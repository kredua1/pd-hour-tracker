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
    public class AgencyConfig : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            
            builder.Property(x => x.AgencyName)
                .HasMaxLength(250)
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

            builder.HasMany(x => x.Attendees)
                .WithOne(x => x.Agency);
        }
    }
}
