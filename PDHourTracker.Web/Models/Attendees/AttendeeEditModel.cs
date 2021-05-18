using PDHourTracker.Web.Models.Agencies;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Attendees
{
    public class AttendeeEditModel : PagingModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Agency required")]
        // Using this to ensure an agency is selected.
        // Default value is 0 that says "Select Agency"
        [Range(1, int.MaxValue, ErrorMessage = "Agency required")]
        public int AgencyId { get; set; }

        [Required(ErrorMessage = "First name required")]
        [StringLength(100, ErrorMessage = "100 characters max")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [StringLength(100, ErrorMessage = "100 characters max")]
        public string LastName { get; set; }

        [StringLength(100, ErrorMessage = "100 characters max")]
        public string MiddleName { get; set; }

        [StringLength(100, ErrorMessage = "100 characters max")]
        public string JobTitle { get; set; }

        [StringLength(36, ErrorMessage = "20 characters max")]
        public string CertId { get; set; }

        public ICollection<AgencyViewModel> Agencies { get; set; }
            = new List<AgencyViewModel>();
    }
}
