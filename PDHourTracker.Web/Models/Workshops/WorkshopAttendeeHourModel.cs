using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Workshops
{
    public class WorkshopAttendeeHourModel
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

        public string FullName { get { return $"{FirstName} {MiddleName} {LastName}"; } }
    }
}
