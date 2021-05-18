using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Attendees
{
    public class AttendeeViewModel : PagingModel
    {
        public int Id { get; set; }

        public int AgencyId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string MiddleName { get; set; }

        public string JobTitle { get; set; }
        public string CertId { get; set; }

        public string FullName { get { return $"{FirstName} {MiddleName} {LastName}"; } }
    }
}
