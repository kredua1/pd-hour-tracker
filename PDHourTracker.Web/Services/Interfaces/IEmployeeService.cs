using PDHourTracker.Web.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IEmployeeService
    {
        /// <summary>
        /// Adds or updates employee. If Id is 0, a new employee is added,
        /// else employee is updated
        /// </summary>
        /// <param name="employeeViewModel"></param>
        void AddOrUpdate(EmployeeViewModel employeeViewModel);

        /// <summary>
        /// Gets employee by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        EmployeeViewModel Get(int id);

        /// <summary>
        /// Gets all employees.
        /// </summary>
        /// <returns></returns>
        List<EmployeeViewModel> GetEmployees();
    }
}
