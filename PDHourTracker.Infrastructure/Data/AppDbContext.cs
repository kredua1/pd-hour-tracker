using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PDHourTracker.Core.Entities;
using PDHourTracker.Infrastructure.Configs;
using PDHourTracker.Infrastructure.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Agency> Agencies { get; set; }
        public DbSet<Attendee> Attendees { get; set; }
        public DbSet<AttendeeHour> AttendeeHours { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProviderCode> ProviderCodes { get; set; }
        public DbSet<SignOutSheetUpload> SignOutSheetUploads { get; set; }
        public DbSet<Workshop> Workshops { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Agency
            modelBuilder.ApplyConfiguration(new AgencyConfig());

            // Attendee
            modelBuilder.ApplyConfiguration(new AttendeeConfig());

            // AttendeeHour
            modelBuilder.ApplyConfiguration(new AttendeeHourConfig());

            // Employee
            modelBuilder.ApplyConfiguration(new EmployeeConfig());

            // Project
            modelBuilder.ApplyConfiguration(new ProjectConfig());

            // ProviderCode
            modelBuilder.ApplyConfiguration(new ProviderCodeConfig());

            // SignOutSheetUpload
            modelBuilder.ApplyConfiguration(new SignOutSheetUploadConfig());

            // Workshop
            modelBuilder.ApplyConfiguration(new WorkshopConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
