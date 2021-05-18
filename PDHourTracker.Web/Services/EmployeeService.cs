using AutoMapper;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Extensions;
using PDHourTracker.Web.Models.Employees;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepo<Employee> _employeeRepo;
        private IMapper _mapper;

        public EmployeeService(IEmployeeRepo<Employee> employeeRepo,
            IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
        }

        public void AddOrUpdate(EmployeeViewModel employeeViewModel)
        {
            if (employeeViewModel.Id == 0)
                _employeeRepo.Add(_mapper.Map<Employee>(employeeViewModel));
            else
                _employeeRepo.Update(_mapper.Map<Employee>(employeeViewModel));
        }

        public EmployeeViewModel Get(int id)
        {
            var employeeViewModel = new EmployeeViewModel();

            var employee = _employeeRepo.Get(id);

            if (employee != null)
                employeeViewModel = _mapper.Map<EmployeeViewModel>(employee);

            return employeeViewModel;
        }

        public List<EmployeeViewModel> GetEmployees()
        {
            var employeeViewModels = new List<EmployeeViewModel>();

            // It's not expected that empolyees will be many
            var employees = _employeeRepo.GetEntities(1, 50);

            employeeViewModels = _mapper.MapList<Employee, EmployeeViewModel>(employees);
            
            return employeeViewModels;
        }
    }
}
