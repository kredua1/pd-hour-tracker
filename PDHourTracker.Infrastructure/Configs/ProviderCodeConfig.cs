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
    public class ProviderCodeConfig : IEntityTypeConfiguration<ProviderCode>
    {
        public void Configure(EntityTypeBuilder<ProviderCode> builder)
        {
            builder.Property(x => x.Code)
                .HasMaxLength(20)
                .IsRequired();
            builder.Property(x => x.StartDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(x => x.EndDate)
                .HasColumnType("date")
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
