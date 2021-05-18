using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.AttendeeHours
{
    public class AttendeeHourBaseModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Attendee required")]
        public int AttendeeId { get; set; }

        [Required(ErrorMessage = "Workshop required")]
        public int WorkshopId { get; set; }

        [Required(ErrorMessage = "Professional development hours required")]
        [Range(0.25, 99, ErrorMessage = "Hours must be between 0.25 and 99")]
        public decimal PDHours { get; set; }

        [StringLength(500, ErrorMessage = "500 characters max")]
        public string Comments { get; set; }
    }
}
