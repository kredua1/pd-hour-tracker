using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class AttendeeHourRepo<TEntity> : BaseRepo<TEntity>, IAttendeeHourRepo<TEntity>
        where TEntity : class
    {
        public AttendeeHourRepo(AppDbContext dbContext)
            : base(dbContext) { }

        /// <summary>
        /// Gets attendee hour by attendee Id and workshop Id
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public AttendeeHour GetByAttendeeAndWorkshop(int attendeeId, int workshopId)
        {
            return _dbContext.AttendeeHours
                .FirstOrDefault(a => a.AttendeeId == attendeeId
                    && a.WorkshopId == workshopId);
        }

        /// <summary>
        /// Returns true/false if attendee has already been assigned to workshop
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public bool Exists(int attendeeId, int workshopId)
        {
            return _dbContext.AttendeeHours
                .Any(ah => ah.WorkshopId == workshopId
                    && ah.AttendeeId == attendeeId);
        }
    }
}
