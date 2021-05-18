using PDHourTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IAttendeeRepo<TEntity> : IBaseRepo<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets all attendees for given agency Id.
        /// </summary>
        /// <param name="agencyId"></param>
        /// <returns></returns>
        List<Attendee> GetByAgency(int agencyId);

        /// <summary>
        /// Gets attendees for given workship Id.
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        List<Attendee> GetByWorkshop(int workshopId);

        /// <summary>
        /// Returns true/false if attendee exists or not.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Exists(string firstName, string lastName, string middleName);

        /// <summary>
        /// Searches attendees on both first and last names for any that
        /// contain given search value
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Attendee> Search(string searchValue);

        List<Attendee> GetAttendees(int pageNum, int pageSize);

        bool CertIdExists(string certId);
        
    }
}
