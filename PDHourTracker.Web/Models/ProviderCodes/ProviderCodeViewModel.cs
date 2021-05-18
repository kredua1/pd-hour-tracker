using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.ProviderCodes
{
    public class ProviderCodeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Provider code required")]
        [StringLength(20, ErrorMessage = "20 characters max")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Start date required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date required")]
        public DateTime EndDate { get; set; }
    }
}
