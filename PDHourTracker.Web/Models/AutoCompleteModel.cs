using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models
{
    /// <summary>
    /// This model contains the two default values used in the jQuery Autocomplete widget.
    /// </summary>
    public class AutoCompleteModel
    {
        public int value { get; set; }
        public string label { get; set; }
    }
}
