using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PDHourTracker.Infrastructure.Identity;
using PDHourTracker.Web.Extensions;
using PDHourTracker.Web.Models.Users;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class UserService : IUserService
    {
        UserManager<ApplicationUser> _userManager;
        private IMapper _mapper;

        public UserService(UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all users orderd by last name, first name
        /// </summary>
        public List<ApplicationUser> Users
        {
            get {
                return _userManager.Users
                  .OrderBy(u => u.LastName)
                  .ThenBy(u => u.FirstName)
                  .ToList();
            }
        }

        /// <summary>
        /// Gets a list of all users with any roles assigned to them
        /// </summary>
        /// <returns></returns>
        public List<UserViewModel> UsersWithRoles()
        {
            var userModels = new List<UserViewModel>();

            //var users = _mapper.MapList<ApplicationUser, UserViewModel>(this.Users);
            var users = this.Users;

            // Loop through users and get possible roles: Admin and Manager
            foreach(var user in users)
            {
                //var roles = new List<string>();
                var userModel = _mapper.Map<UserViewModel>(user);

                // Check admin role
                userModel.HasAdminRole = _userManager.IsInRoleAsync(user, UserRoles.Admin).Result;
                //if (_userManager.IsInRoleAsync(user, UserRoles.Admin).Result)
                //roles.Add(UserRoles.Admin);
                // Check manager role
                userModel.HasManagerRole = _userManager.IsInRoleAsync(user, UserRoles.Manager).Result;
                //if (_userManager.IsInRoleAsync(user, UserRoles.Manager).Result)
                //    roles.Add(UserRoles.Admin);


                //userModel.Roles = roles;
                userModels.Add(userModel);
            }

            return userModels;
        }

        /// <summary>
        /// Returns any users whose first name, last name or email
        /// contains the given search string.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<ApplicationUser> Search(string searchValue)
        {
            return _userManager.Users
                .Where(u => u.LastName.Contains(searchValue)
                    || u.FirstName.Contains(searchValue)
                    || u.Email.Contains(searchValue))
                .OrderBy(u => u.LastName)
                .ThenBy(u => u.FirstName)
                .ToList();
        }

        /// <summary>
        /// Adds user with given user id to given role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        public bool AddToRole(string userId, string roleName)
        {
            var added = true;

            var user = _userManager.FindByIdAsync(userId).Result;

            if(user != null && !string.IsNullOrEmpty(user.Id))
            {
                var result = _userManager.AddToRoleAsync(user, roleName).Result;
                added = result.Succeeded;
            }

            return added;
        }

        /// <summary>
        /// Removes user with given id from given role.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public bool RemoveFromRole(string userId, string roleName)
        {
            var removed = true;

            var user = _userManager.FindByIdAsync(userId).Result;

            if(user != null && !string.IsNullOrEmpty(user.Id))
            {
                var result = _userManager.RemoveFromRoleAsync(user, roleName).Result;
                removed = result.Succeeded;
            }

            return removed;
        }
    }
}
