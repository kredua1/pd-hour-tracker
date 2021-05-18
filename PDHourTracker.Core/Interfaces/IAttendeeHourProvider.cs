using PDHourTracker.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IAttendeeHourProvider
    {
        /// <summary>
        /// Gets all workshops for given attendee Id.
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        List<AttendeeWorkshopHour> GetAttendeeWorkshopHours(int attendeeId);

        /// <summary>
        /// Gets all attendees with PD hours for a workshop.
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        List<WorkshopAttendeeHour> GetWorkshopAttendeeHours(int workshopId);
    }
}
