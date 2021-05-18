using PDHourTracker.Web.Models.Workshops;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Projects
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Project name required")]
        [StringLength(250, ErrorMessage = "250 characters max")]
        public string ProjectName { get; set; }

        [StringLength(250, ErrorMessage = "500 characters max")]
        public string Description { get; set; }

        public ICollection<WorkshopViewModel> Workshops { get; set; }
            = new List<WorkshopViewModel>();
    }
}
