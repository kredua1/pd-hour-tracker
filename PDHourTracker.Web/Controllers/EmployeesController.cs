using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDHourTracker.Web.Models.Employees;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Controllers
{
    /// <summary>
    /// This class lists and adds/updates Partners employees.
    /// Each project is assigned one employee as the contact.
    /// </summary>
    [Authorize(Roles = UserRoles.AdminOrManager)]
    public class EmployeesController : Controller
    {
        private IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Default action. Lists all employees
        public ActionResult Index()
        {
            return View(_employeeService.GetEmployees());
        }

        // Get. Add Employee
        public ActionResult Add()
        {
            return View(new EmployeeViewModel());
        }

        // Post. Add Employee
        [HttpPost]
        public ActionResult Add(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Add employee to db
            _employeeService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }

        // Update employee : Get
        public ActionResult Update(int id = 0)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));

            // Get employee from db
            var model = _employeeService.Get(id);

            // Check for valid employee by checking id
            if (model.Id <= 0)
                return RedirectToAction(nameof(Index));

            return View(model);
        }

        // Update employee : Post
        [HttpPost]
        public ActionResult Update(EmployeeViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Update employee in db
            _employeeService.AddOrUpdate(model);

            return RedirectToAction(nameof(Index));
        }
    }
}
