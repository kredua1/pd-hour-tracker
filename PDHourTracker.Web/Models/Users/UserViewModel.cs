using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Users
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool HasAdminRole { get; set; }
        public bool HasManagerRole { get; set; }

        public string FullName { get { return $"{FirstName} {Lastname}"; } }

        //public IList<string> Roles { get; set; }
        //    = new List<string>();

        ///// <summary>
        ///// Returns roles as a comma separated string
        ///// </summary>
        //public string ToRolesCsv
        //{
        //    get
        //    {
        //        var csv = "";
        //        foreach(var role in Roles)
        //        {
        //            if (!string.IsNullOrEmpty(csv))
        //                csv += ",";
        //            csv += role;
        //        }
        //        return csv;
        //    }
        //}
    }
}
