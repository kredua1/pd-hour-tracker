using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Api
{
    /// <summary>
    /// Currently not in use.
    /// </summary>
    [Route("api/projects")]
    [ApiController]
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class ProjectController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ProjectViewModel> GetProjects()
        {
            return GetProjectsInMemory();
        }

        #region Helpers

        private IEnumerable<ProjectViewModel> GetProjectsInMemory()
        {
            return new List<ProjectViewModel>
            {
                new ProjectViewModel
                {
                    Id = 1,
                    ProjectName = "Arkansas Disability and Health Program",
                    Description = "Health program for people with disabilities"
                },
                new ProjectViewModel
                {
                    Id = 2,
                    ProjectName = "Welcome the Children",
                    Description = "Children with disabilities"
                }
            };
        }
        #endregion
    }
}
