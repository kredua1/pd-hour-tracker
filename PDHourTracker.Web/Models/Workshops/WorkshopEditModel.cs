using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Workshops
{
    public class WorkshopEditModel : PagingModel
    {
        public WorkshopEditModel()
        {
            Projects = new List<Projects.ProjectViewModel>();
            Employees = new List<Employees.EmployeeViewModel>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Workshop name required")]
        [StringLength(250, ErrorMessage = "250 characters max")]
        public string WorkshopName { get; set; }

        [StringLength(500, ErrorMessage = "500 characters max")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Training date required")]
        [DataType(DataType.Date)]
        public DateTime TrainingDate { get; set; }

        [Required(ErrorMessage = "Session identifier required")]
        [StringLength(50, ErrorMessage = "50 characters max")]
        public string SessionIdentifier { get; set; }

        [Required(ErrorMessage = "PD hours required")]
        public decimal PDHours { get; set; }

        [Required(ErrorMessage = "Project required")]
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Employee required")]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Provider code required")]
        public int ProviderCodeId { get; set; }

        [StringLength(500, ErrorMessage = "500 characters max")]
        public string Comments { get; set; }

        // Provider code will be appended to given session identifier
        //public string ProviderCode { get; set; }

        // List of projects to select from to assign to workshop
        public ICollection<Projects.ProjectViewModel> Projects { get; set; }

        // List of employees to select from to assign to workshop
        public ICollection<Employees.EmployeeViewModel> Employees { get; set; }

        // List of provider codes to select from to append to Session Identifier
        public SelectList ProviderCodes { get; set; }
    }
}
