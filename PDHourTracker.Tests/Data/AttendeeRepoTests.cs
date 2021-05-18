using NUnit.Framework;
using PDHourTracker.Core.Entities;
using PDHourTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Tests.Data
{
    [TestFixture]
    public class AttendeeRepoTests
    {
        [Test]
        public void CanAddAttendee()
        {
            using(var dbContext = SqliteInMemory.GetSqliteDbContext())
            {
                dbContext.Database.EnsureCreated();
                var attendeeRepo = new AttendeeRepo<Attendee>(dbContext);
            }
        }

        [Test]
        public void CanGetWorkshopAttendees()
        {
            using (var dbContext = SqliteInMemory.GetSqliteDbContext())
            {
                dbContext.Database.EnsureCreated();

                var attendeeRepo = new AttendeeRepo<Attendee>(dbContext);


                var project = new Project
                {
                    ProjectName = "Arkansas Disability and Health Project",
                    Description = "Health education for people with disabilities"
                };
                dbContext.Projects.Add(project);
                var employee = new Employee { FirstName = "John", LastName = "Employee" };
                dbContext.Employees.Add(employee);
                dbContext.SaveChanges();

                var workshop1 = new Workshop
                {
                    WorkshopName = "Women Be Healthy",
                    EmployeeId = employee.Id,
                    ProjectId = project.Id,
                    Description = "Women's health class",
                    TrainingDate = DateTime.Now,
                    SessionIdentifier = "WBH-4181234500000"
                };
                var workshop2 = new Workshop
                {
                    WorkshopName = "Diabetes",
                    EmployeeId = employee.Id,
                    ProjectId = project.Id,
                    Description = "Diabetes health class",
                    TrainingDate = DateTime.Now,
                    SessionIdentifier = "DBH-4181234500000"
                };
                dbContext.Workshops.AddRange(workshop1, workshop2);

                var agency = new Agency { AgencyName = "Little Rock High School" };
                dbContext.Agencies.Add(agency);
                dbContext.SaveChanges();

                var attendee1 = new Attendee
                {
                    FirstName = "Sarah",
                    LastName = "Smith",
                    JobTitle = "Supervisor",
                    AgencyId = agency.Id
                };
                var attendee2 = new Attendee
                {
                    FirstName = "Andy",
                    LastName = "Pablo",
                    JobTitle = "Manager",
                    AgencyId = agency.Id
                };
                dbContext.Attendees.AddRange(attendee1, attendee2);
                dbContext.SaveChanges();

                dbContext.AttendeeHours.Add(new AttendeeHour
                {
                    AttendeeId = attendee1.Id,
                    WorkshopId = workshop1.Id,
                    PDHours = 1
                });
                dbContext.AttendeeHours.Add(new AttendeeHour
                {
                    AttendeeId = attendee2.Id,
                    WorkshopId = workshop1.Id,
                    PDHours = 1
                });
                dbContext.SaveChanges();

                var attendees = attendeeRepo.GetByWorkshop(workshop1.Id);

                Assert.AreEqual(2, attendees.Count);
            }
        }
    }
}
