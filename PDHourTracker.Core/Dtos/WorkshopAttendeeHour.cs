using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Dtos
{
    public class WorkshopAttendeeHour
    {
        public int AttendeeHourId { get; set; }
        public int AttendeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public decimal PDHours { get; set; }
        public string JobTitle { get; set; }
        public string AgencyName { get; set; }
        public string CertId { get; set; }
    }
}
