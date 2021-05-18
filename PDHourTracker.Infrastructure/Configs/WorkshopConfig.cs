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
    public class WorkshopConfig : IEntityTypeConfiguration<Workshop>
    {
        public void Configure(EntityTypeBuilder<Workshop> builder)
        {
            builder.Property(x => x.WorkshopName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(x => x.TrainingDate)
                .HasColumnType("date")
                .IsRequired();
            builder.Property(x => x.SessionIdentifier)
                .HasMaxLength(50)
                .IsRequired();
            builder.Property(x => x.PDHours)
                .HasColumnType("decimal(4,2)")
                .IsRequired();
            builder.Property(x => x.ProjectId)
                .IsRequired();
            builder.Property(x => x.EmployeeId)
                .IsRequired();
            builder.Property(x => x.ProviderCodeId)
                .IsRequired();
            builder.Property(x => x.Description)
                .HasMaxLength(500);
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
