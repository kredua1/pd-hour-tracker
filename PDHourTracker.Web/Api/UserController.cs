using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.Users;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Api
{
    /// <summary>
    /// Controller is used to add/remove user roles.
    /// </summary>
    [ApiController]
    [Route("/api/users")]
    [Authorize(Roles = UserRoles.Admin)]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // Update user role : Post
        [HttpPost]
        [Route("/api/users/updaterole")]
        public ActionResult UpdateRole(UserRoleModel model)
        {
            var result = false;
            if(model.Add)
                result = _userService.AddToRole(model.UserId, model.RoleName);
            else
                result = _userService.RemoveFromRole(model.UserId, model.RoleName);

            return Ok(result);
        }

        // Add user to role : Post
        // Model should have user id and role name
        [HttpPost]
        [Route("/api/users/addrole")]
        public ActionResult AddRole(UserRoleModel model)
        {
            var result = _userService.AddToRole(model.UserId, model.RoleName);
            return Ok(result);
        }

        // Remove user from role : Post
        // Model should have user id and role name
        [HttpPost]
        [Route("/api/users/removerole")]
        public ActionResult RemoveRole(UserRoleModel model)
        {
            var result = _userService.RemoveFromRole(model.UserId, model.RoleName);
            return Ok(result);
        }
    }
}
