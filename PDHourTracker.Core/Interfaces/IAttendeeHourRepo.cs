using PDHourTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IAttendeeHourRepo<TEntity> : IBaseRepo<TEntity>
        where TEntity : class
    {
        AttendeeHour GetByAttendeeAndWorkshop(int attendeeId, int workshopId);

        /// <summary>
        /// Returns true/false if attendee has already been assigned to workshop
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        bool Exists(int attendeeId, int workshopId);
    }
}
