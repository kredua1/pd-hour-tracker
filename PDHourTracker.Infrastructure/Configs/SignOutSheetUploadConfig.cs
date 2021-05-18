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
    public class SignOutSheetUploadConfig : IEntityTypeConfiguration<SignOutSheetUpload>
    {
        public void Configure(EntityTypeBuilder<SignOutSheetUpload> builder)
        {
            builder.Property(s => s.WorkshopId)
                .IsRequired();
            builder.Property(s => s.FileName)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(s => s.FileSize)
                .IsRequired();
            builder.Property(s => s.ContentType)
                .HasMaxLength(250)
                .IsRequired();
            builder.Property(s => s.FileData)
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
