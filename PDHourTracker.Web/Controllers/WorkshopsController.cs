using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Helpers;
using PDHourTracker.Web.Models.Employees;
using PDHourTracker.Web.Models.Workshops;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This controller lists/adds/updates workshops.
    /// It also provides an Excel download of attendees for a workshop.
    /// </summary>
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class WorkshopsController : BaseController
    {
        private IWorkshopService _workshopService;
        private IProjectService _projectService;
        private IProviderCodeService _providerCodeService;
        private IEmployeeService _employeeService;
        private IExcelService _excelService;
        private ISignOutSheetUploadService _signOutSheetUploadService;
        

        public WorkshopsController(IWorkshopService workshopService,
            IProjectService projectService,
            IEmployeeService employeeService,
            IProviderCodeService providerCodeService,
            IExcelService excelService,
            ISignOutSheetUploadService signOutSheetUploadService)
        {
            _workshopService = workshopService;
            _projectService = projectService;
            _employeeService = employeeService;
            _providerCodeService = providerCodeService;
            _excelService = excelService;
            _signOutSheetUploadService = signOutSheetUploadService;
        }

        // Default view. Shows workshops with option to filter by project.
        public ActionResult Index(int p = 1, int ps = 50, string sv = "")
        {
            StorePaging(p, ps, sv);

            var model = new WorkshopListModel();
            model.Pager = new Models.TableFooterPagingModel(
                PageNumber,
                PageSize,
                _workshopService.Total(),
                "Workshops", "Index");
            // Set number of columns to span for paging links and item counts
            model.Pager.PageLinkColSpan = 4;
            model.Pager.ItemCountColspan = 1;

            model.Workshops = _workshopService.GetWorkshops(PageNumber, PageSize, SearchValue);

            return View(model);
        }

        // Create workshop. Get
        // pid is project Id. Allows project to be selected
        public ActionResult Add(int pid = 0)
        {
            var model = new WorkshopEditModel();

            // Add list of projects and employees to choose from
            LoadListsForEdit(model);

            // If given project id (pid) exists, set ProjectId so it's selected initially
            if (pid > 0 && _projectService.Exists(pid))
            {
                model.ProjectId = pid;
            }

            // Default date to today's
            model.TrainingDate = DateTime.Now;

            // Pass the action to the form so it knows to return to Add
            ViewData["Action"] = "Add";

            return View(model);
        }

        // Create workshop. Post
        [HttpPost]
        public ActionResult Add(WorkshopEditModel model)
        {
            if(!ModelState.IsValid)
            {
                // Pass the action to the form so it knows to return to Add
                ViewData["Action"] = "Add";

                // Add list of projects and employees to choose from
                LoadListsForEdit(model);

                return View(model);
            }

            // Provider code must be unique. Validate
            if (!_workshopService.SessionIdentifierIsUnique(model.SessionIdentifier))
            {
                ModelState.AddModelError("", "Session Identifier already used. "
                    + " Please enter a unique session identifier.");

                // Pass the action to the form so it knows to return to Add
                ViewData["Action"] = "Add";

                // Add list of projects and employees to choose from
                LoadListsForEdit(model);

                return View(model);
            }

            // Add workshop to db
            model = _workshopService.AddOrUpdate(model);

            return RedirectToAction(nameof(Details), new { id = model.Id });
        }

        // Update workshop : Get
        public ActionResult Update(int id = 0, int p = 1, int ps = 50)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            

            var model = _workshopService.GetForEdit(id);
            model.Id = id;
            LoadListsForEdit(model);

            // Save Paging info
            StorePaging(p, ps);
            model.PageNum = PageNumber;
            model.PageSize = PageSize;

            // Pass the action to the form so it knows to return to Update
            ViewData["Action"] = "Update";

            return View(model);
        }

        // Update workshop : Post
        [HttpPost]
        public ActionResult Update(WorkshopEditModel model)
        {
            if (!ModelState.IsValid)
            {
                // Pass the action to the form so it knows to return to Update
                ViewData["Action"] = "Update";
                LoadListsForEdit(model);
                return View(model);
            }

            // Provider code must be unique. Validate
            if (!_workshopService.SessionIdentifierIsUnique(model.SessionIdentifier, model.Id))
            {
                ModelState.AddModelError("", "Session Identifier already used. "
                    + " Please enter a unique session identifier.");

                // Pass the action to the form so it knows to return to Update
                ViewData["Action"] = "Update";

                // Add list of projects and employees to choose from
                LoadListsForEdit(model);

                return View(model);
            }

            // Save changes to db
            _workshopService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }

        // Workshop details: name, project, employee and list of attendees with 
        // professional development hours.
        public ActionResult Details(int id = 0, int p = 1, int ps = 50)
        {
            var pageNumber = PagingHelpers.ValidatePageNumber(p);
            var pageSize = PagingHelpers.ValidatePageSize(ps);

            if (id == 0)
            {
                return RedirectToAction(nameof(Index), new
                {
                    p = pageNumber,
                    ps = pageSize
                });
            }

            // Store page number and size for return to list of workshops
            ViewData["PageNumber"] = pageNumber;
            ViewData["PageSize"] = pageSize;

            // Get the workshop and the employee/contact and project
            var workshopViewModel = _workshopService.Get(id);
            workshopViewModel.Employee = _employeeService.Get(workshopViewModel.EmployeeId);
            workshopViewModel.Project = _projectService.Get(workshopViewModel.ProjectId);
            workshopViewModel.ProviderCode = _providerCodeService.Get(workshopViewModel.ProviderCodeId);

            // Get sign-out sheet Id's 
            workshopViewModel.SignOutSheets = _signOutSheetUploadService.GetIdsByWorkshop(id);

            // Get all attendees with professional development hours
            workshopViewModel.WorkshopAttendeeHours = _workshopService.GetWorkshopAttendeeHours(id);

            return View(workshopViewModel);
        }

        // Get workshops that employee is the contact for : Get
        // id is employee Id
        public ActionResult ByEmployee(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            var model = _employeeService.Get(id);
            model.Workshops = _workshopService.ByEmployee(id);

            return View(model);
        }

        // Get workshops by project : Get
        // id is project id
        public ActionResult ByProject(int id = 0)
        {
            if(id <= 0)
                return RedirectToAction(nameof(Index));

            var model = _projectService.Get(id);
            model.Workshops = _workshopService.ByProject(id);

            return View(model);
        }

        // Get attendee PD hours as Excel spreadsheet : Get
        // id is workshop id
        public ActionResult PDHoursExcel(int id = 0)
        {
            if (id == 0)
                return null;

            // Get byte array from excel service
            var fileBytes = _excelService.WorkshopAttendeeHours(id);

            if (fileBytes == null)
                return null;

            return File(fileBytes, MimeTypes.Excel, "PDHours.xlsx");
        }

        #region Helpers
        private void LoadListsForEdit(WorkshopEditModel model)
        {
            model.Employees = _employeeService.GetEmployees();
            model.Projects = _projectService.GetProjects();
            model.ProviderCodes = _providerCodeService.ProviderCodesForView();
        }

        private string ValidateSessionIdentifier(string sessionIdentifier, string providerCode)
        {
            // Session identifier must include provider code and be no more than 50 characters
            // Session identifier without provider code must be no more than 30 characters
            sessionIdentifier = sessionIdentifier.Replace(providerCode, "");
            if(sessionIdentifier.Length > 30)
            {
                sessionIdentifier = sessionIdentifier.Substring(0, 30);
            }
            sessionIdentifier = sessionIdentifier + providerCode;

            return sessionIdentifier;
        }
        #endregion
    }
}
