using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Dtos
{
    public class AttendeeWorkshopHour
    {
        public int WorkshopId { get; set; }
        public string WorkshopName { get; set; }
        public DateTime TrainingDate { get; set; }
        public decimal PDHours { get; set; }
    }
}
