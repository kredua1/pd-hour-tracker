using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class AttendeeRepo<TEntity> : BaseRepo<TEntity>, IAttendeeRepo<TEntity>
        where TEntity : class
    {
        public AttendeeRepo(AppDbContext dbContext)
            : base(dbContext) { }

        /// <summary>
        /// Gets all attendees for given agency Id.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        public List<Attendee> GetByAgency(int agencyId)
        {
            return _dbContext.Agencies
                .Where(a => a.Id == agencyId)
                .SelectMany(a => a.Attendees)
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .ToList();
        }

        /// <summary>
        /// Gets attendees for given workship Id.
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public List<Attendee> GetByWorkshop(int workshopId)
        {
            return _dbContext.AttendeeHours
                .Where(ah => ah.WorkshopId == workshopId)
                .Select(ah => ah.Attendee)
                .ToList();
        }

        /// <summary>
        /// Returns true/false if attendee exists or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(string firstName, string lastName, string middleName)
        {
            return _dbContext.Attendees.Any(
                a => a.FirstName == firstName
                && a.LastName == lastName
                && a.MiddleName == middleName);
        }

        /// <summary>
        /// Searches attendees on both first, last and middle names
        /// for any that contain given search value
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Attendee> Search(string searchValue)
        {
            return _dbContext.Attendees
                .Where(a => a.FirstName.Contains(searchValue)
                    || a.LastName.Contains(searchValue)
                    || a.MiddleName.Contains(searchValue))
                .ToList();
        }

        /// <summary>
        /// Gets attendees paged ordered by last name and then first name.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Attendee> GetAttendees(int pageNum, int pageSize)
        {
            return _dbContext.Attendees
                .OrderBy(a => a.LastName)
                .ThenBy(a => a.FirstName)
                .Skip((pageNum - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }

        /// <summary>
        /// Returns true/false attendee has given CertId.
        /// </summary>
        /// <param name="certId"></param>
        /// <returns></returns>
        public bool CertIdExists(string certId)
        {
            return _dbContext.Attendees
                .Any(a => a.CertId == certId);
        }
    }
}
