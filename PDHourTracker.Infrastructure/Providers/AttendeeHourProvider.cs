using PDHourTracker.Core.Dtos;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Providers
{
    public class AttendeeHourProvider : IAttendeeHourProvider
    {
        private AppDbContext _dbContext;

        public AttendeeHourProvider(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets workshop name, training date and attendee PD hours for workshop
        /// ordered by most recent workshop.
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        public List<AttendeeWorkshopHour> GetAttendeeWorkshopHours(int attendeeId)
        {
            return (from ah in _dbContext.AttendeeHours
                    where ah.AttendeeId == attendeeId
                    join w in _dbContext.Workshops
                    on ah.WorkshopId equals w.Id
                    orderby w.TrainingDate descending
                    select new AttendeeWorkshopHour
                    {
                        WorkshopId = w.Id,
                        WorkshopName = w.WorkshopName,
                        TrainingDate = w.TrainingDate,
                        PDHours = ah.PDHours
                    })
                    .ToList();
        }

        /// <summary>
        /// Gets all attendees with PD hours for a workshop.
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public List<WorkshopAttendeeHour> GetWorkshopAttendeeHours(int workshopId)
        {
            return (from ah in _dbContext.AttendeeHours
                    where ah.WorkshopId == workshopId
                    join a in _dbContext.Attendees
                    on ah.AttendeeId equals a.Id
                    join ag in _dbContext.Agencies
                    on a.AgencyId equals ag.Id
                    orderby a.LastName, a.FirstName
                    select new WorkshopAttendeeHour
                    {
                        AttendeeHourId = ah.Id,
                        AttendeeId = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        MiddleName = a.MiddleName,
                        PDHours = ah.PDHours,
                        JobTitle = a.JobTitle,
                        AgencyName = ag.AgencyName,
                        CertId = a.CertId
                    })
                    .ToList();
        }
    }
}
