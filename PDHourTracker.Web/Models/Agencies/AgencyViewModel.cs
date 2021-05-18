using PDHourTracker.Web.Models.Attendees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Agencies
{
    public class AgencyViewModel : PagingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Agency name required")]
        [StringLength(250, ErrorMessage = "250 characters max")]
        public string AgencyName { get; set; }

        public ICollection<AttendeeViewModel> Attendees { get; set; }
             = new List<AttendeeViewModel>();
    }
}
