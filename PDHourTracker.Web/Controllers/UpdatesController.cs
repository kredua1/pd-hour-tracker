using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This controller provides one view with links to view/add:
    /// Projects, Provider Codes and Employees
    /// </summary>
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class UpdatesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
