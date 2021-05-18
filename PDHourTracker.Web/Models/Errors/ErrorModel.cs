using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Models.Errors
{
    public class ErrorModel
    {
        public string ErrorMessage { get; set; }
        public string RouteOfException { get; set; }
        public string ExceptionError { get; set; }
    }
}
