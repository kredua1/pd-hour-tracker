using PDHourTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Dtos
{
    /// <summary>
    /// This class has all the properties of an Attendee entity along with
    /// Agency name so Attendees and Agencies can be joined for reporting.
    /// </summary>
    public class AttendeeWithAgency : Attendee
    {
        public string AgencyName { get; set; }
    }
}
