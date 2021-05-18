using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Users
{
    public class UserRoleModel
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }

        /// <summary>
        /// If set to true, role is added.
        /// If set to false, role is removed.
        /// </summary>
        public bool Add { get; set; }
    }
}
