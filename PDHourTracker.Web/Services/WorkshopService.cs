using AutoMapper;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Enums;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Extensions;
using PDHourTracker.Web.Models.Employees;
using PDHourTracker.Web.Models.Projects;
using PDHourTracker.Web.Models.ProviderCodes;
using PDHourTracker.Web.Models.Workshops;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class WorkshopService : IWorkshopService
    {
        IWorkshopRepo<Workshop> _workshopRepo;
        private IAttendeeHourProvider _attendeeHourProvider;
        IMapper _mapper;

        public WorkshopService(IWorkshopRepo<Workshop> workshopRepo,
            IAttendeeHourProvider attendeeHourProvider,
            IMapper mapper)
        {
            _workshopRepo = workshopRepo;
            _attendeeHourProvider = attendeeHourProvider;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates new workshop if Id is zero; otherwise updates workshop.
        /// </summary>
        /// <param name="workshopEditModel"></param>
        public WorkshopEditModel AddOrUpdate(WorkshopEditModel workshopEditModel)
        {
            var workshop = _mapper.Map<Workshop>(workshopEditModel);

            if (workshopEditModel.Id == 0)
            {
                _workshopRepo.Add(workshop);
                // Get Id from new workshop
                workshopEditModel.Id = workshop.Id;
            }
            else
                _workshopRepo.Update(workshop);

            return workshopEditModel;
        }

        /// <summary>
        /// Returns workshops that employee is the contact for.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<WorkshopViewModel> ByEmployee(int employeeId)
        {
            var workshopViewModels = new List<WorkshopViewModel>();

            var workshops = _workshopRepo.ByEmployee(employeeId);

            workshopViewModels = _mapper.MapList<Workshop, WorkshopViewModel>
                (workshops);

            return workshopViewModels;
        }

        /// <summary>
        /// Returns workshops for given project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<WorkshopViewModel> ByProject(int projectId)
        {
            var workshopViewModels = new List<WorkshopViewModel>();

            var workshops = _workshopRepo.ByProject(projectId);

            workshopViewModels = _mapper.MapList<Workshop, WorkshopViewModel>
                (workshops);

            return workshopViewModels;
        }

        /// <summary>
        /// Gets workshop by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkshopViewModel Get(int id)
        {
            var workshopViewModel = new WorkshopViewModel();

            var workshop = _workshopRepo.Get(id);

            if (workshop != null)
                workshopViewModel = _mapper.Map<WorkshopViewModel>(workshop);

            return workshopViewModel;
        }

        /// <summary>
        /// Gets workshop along with all related entities: Project, Employee, Provider Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkshopViewModel GetDetails(int id)
        {
            var workshopViewModel = new WorkshopViewModel();

            var workshop = _workshopRepo.GetDetails(id);

            if(workshop != null)
            {
                workshopViewModel = _mapper.Map<WorkshopViewModel>(workshop);
                workshopViewModel.Project = _mapper.Map<ProjectViewModel>(workshop.Project);
                workshopViewModel.Employee = _mapper.Map<EmployeeViewModel>(workshop.Employee);
                workshopViewModel.ProviderCode = _mapper.Map<ProviderCodeViewModel>(
                    workshop.ProviderCode);
            }

            return workshopViewModel;
        }

        /// <summary>
        /// Gets workshop edit view model.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public WorkshopEditModel GetForEdit(int id)
        {
            var workshopEditModel = new WorkshopEditModel();

            var workshop = _workshopRepo.Get(id);

            if (workshop != null)
                workshopEditModel = _mapper.Map<WorkshopEditModel>(workshop);

            return workshopEditModel;
        }

        public List<WorkshopAttendeeHourModel> GetWorkshopAttendeeHours(int workshopId)
        {
            var workshopAttendeeHourModels = new List<WorkshopAttendeeHourModel>();

            var workshopAttendeeHours = _attendeeHourProvider
                .GetWorkshopAttendeeHours(workshopId);

            foreach(var workshopAttendeeHour in workshopAttendeeHours)
            {
                workshopAttendeeHourModels.Add(
                    _mapper.Map<WorkshopAttendeeHourModel>(workshopAttendeeHour));
            }

            return workshopAttendeeHourModels;
        }

        /// <summary>
        /// Gets paged list of workshops.
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<WorkshopViewModel> GetWorkshops(int pageNumber, int pageSize, string searchValue)
        {
            var workshopViewModels = new List<WorkshopViewModel>();

            var workshops = new List<Workshop>();

            // If a search string is given, search
            if (!string.IsNullOrEmpty(searchValue))
                workshops = _workshopRepo.Search(searchValue, pageNumber, pageSize);
            else
                workshops = _workshopRepo.GetEntities(w => w.WorkshopName, Sorted.ASC, pageNumber, pageSize);

            // If any workshops in results, map the entities to view models
            if (workshops.Any())
            {
                workshopViewModels = _mapper.MapList<Workshop, WorkshopViewModel>(workshops);
            }

            return workshopViewModels;
        }

        /// <summary>
        /// Returns true if given session identifier is not assigned to a workshop
        /// </summary>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public bool SessionIdentifierIsUnique(string sessionIdentifier)
        {
            return _workshopRepo.SessionIdentifierIsUnique(sessionIdentifier);
        }

        /// <summary>
        /// Returns true if session identifier doesn't exist on
        /// workshops other than the one the workshopId is given for.
        /// When a workshop is being updated, the session identifier
        /// would already exist for the workshop being updated.
        /// </summary>
        /// <param name="sessionIdentifier"></param>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public bool SessionIdentifierIsUnique(string sessionIdentifier, int workshopId)
        {
            return _workshopRepo.SessionIdentifierIsUnique(sessionIdentifier, workshopId);
        }

        /// <summary>
        /// Gets the total number of workshops.
        /// </summary>
        /// <returns></returns>
        public int Total()
        {
            return _workshopRepo.Total();
        }

        /// <summary>
        /// Search for workshops by name. Returns all workshops with names
        /// containing the given search value.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<WorkshopViewModel> Search(string searchValue, int pageNumber, int pageSize)
        {
            var workshopModels = new List<WorkshopViewModel>();

            // Search workshops
            var workshops = _workshopRepo.Search(searchValue, pageNumber, pageSize);

            // If any results, map the entites to models
            if (workshops.Any())
            {
                workshopModels = _mapper.MapList<Workshop, WorkshopViewModel>(workshops);
            }

            return workshopModels;
        }
    }
}
