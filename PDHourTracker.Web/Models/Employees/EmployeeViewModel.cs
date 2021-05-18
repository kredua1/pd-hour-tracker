using PDHourTracker.Web.Models.Workshops;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Employees
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First name required")]
        [StringLength(100, ErrorMessage = "100 characters max")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name required")]
        [StringLength(100, ErrorMessage = "100 characters max")]
        public string LastName { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public ICollection<WorkshopViewModel> Workshops
            = new List<WorkshopViewModel>();
    }
}
