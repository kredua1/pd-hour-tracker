using PDHourTracker.Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Entities
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            Workshops = new HashSet<Workshop>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName { get { return $"{FirstName} {LastName}"; } }

        public virtual ICollection<Workshop> Workshops { get; set; }
    }
}
