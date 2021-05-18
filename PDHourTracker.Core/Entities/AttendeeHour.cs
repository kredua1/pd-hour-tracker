using PDHourTracker.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Entities
{
    public class AttendeeHour : BaseEntity
    {
        public int AttendeeId { get; set; }
        public int WorkshopId { get; set; }
        public decimal PDHours { get; set; }
        public string Comments { get; set; }

        public virtual Attendee Attendee { get; set; }
        public virtual Workshop Workshop { get; set; }
    }
}
