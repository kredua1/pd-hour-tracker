using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Models.Projects;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This controller lists projects and update projects and 
    /// allows new ones to be added.
    /// </summary>
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class ProjectsController : Controller
    {
        private IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public ActionResult Index()
        {
            return View(_projectService.GetProjects());
        }

        // New project
        public ActionResult Add()
        {
            return View(new ProjectViewModel());
        }

        // Create new project
        [HttpPost]
        public ActionResult Add(ProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Add project to db
            _projectService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }

        // Update project : Get
        public ActionResult Update(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            // Get project to update from db
            var model = _projectService.Get(id);

            // Check to ensure we got project by checking id
            if(model.Id <= 0)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        // Update project : Post
        [HttpPost]
        public ActionResult Update(ProjectViewModel model)
        {
            if(!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            // Update project in db
            _projectService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }

        #region Helpers


        #endregion
    }
}
