using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This is the "default" controller. It has the home page - "Index".
    /// </summary>
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class HomeController : Controller
    {
        // Home page : Get
        public IActionResult Index()
        {
            return View();
        }

        // The "pretty" error page : Get
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Will delete this once global error handler is finished
        public IActionResult TestError()
        {
            throw new NotImplementedException();
        }
    }
}
