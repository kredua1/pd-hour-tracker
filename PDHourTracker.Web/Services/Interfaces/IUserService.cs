using PDHourTracker.Infrastructure.Identity;
using PDHourTracker.Web.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IUserService
    {
        List<ApplicationUser> Users { get; }
        List<UserViewModel> UsersWithRoles();
        List<ApplicationUser> Search(string searchValue);
        bool AddToRole(string userId, string roleName);
        bool RemoveFromRole(string userId, string roleName);
    }
}
