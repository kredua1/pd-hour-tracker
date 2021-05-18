using PDHourTracker.Web.Models.AttendeeHours;
using PDHourTracker.Web.Models.Workshops;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IAttendeeHourService
    {
        AttendeeHourViewModel AddOrUpdate(AttendeeHourEditModel attendeeHourEditModel);
        AttendeeHourViewModel Get(int id);
        AttendeeHourEditModel GetForEdit(int id);
        AttendeeHourViewModel GetByAttendeeAndWorkshop(int attendeeId, int workshopId);

        /// <summary>
        /// Returns true/false if attendee has already been assigned to workshop
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        bool Exists(int attendeeId, int workshopId);

        void Delete(int id);

        List<AttendeeWorkshopHourModel> GetAttendeeWorkshopHours(int attendeeId);
    }
}
