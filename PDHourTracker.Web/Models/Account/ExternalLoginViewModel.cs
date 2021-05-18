using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        [StringLength(256, ErrorMessage = "256 characters max")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "50 characters max")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "50 characters max")]
        public string LastName { get; set; }
    }
}
