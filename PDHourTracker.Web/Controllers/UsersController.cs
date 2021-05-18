using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Infrastructure.Identity;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This controller lists users and allows roles to be added/removed.
    /// </summary>
    /// Admin role is required to access
    [Authorize(Roles = UserRoles.Admin)]
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // List users and roles if any : Get
        // Should only be Partners employees so expecting less than 50
        public ActionResult Index()
        {
            var users = _userService.UsersWithRoles();
            return View(users);
        }

        // Searchs for users by given search value : Post
        //[HttpPost]
        //public ActionResult Index([FromForm] string searchValue)
        //{
        //    var users = _userService.Search(searchValue);
        //    return View(users);
        //}
    }
}
