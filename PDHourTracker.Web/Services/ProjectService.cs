using AutoMapper;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Web.Models.Projects;
using PDHourTracker.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepo<Project> _projectRepo;
        private IMapper _mapper;

        public ProjectService(IProjectRepo<Project> projectRepo,
            IMapper mapper)
        {
            _projectRepo = projectRepo;
            _mapper = mapper;
        }

        /// <summary>
        /// Adds new project to db if Id is 0 or updates existing project.
        /// </summary>
        /// <param name="projectViewModel"></param>
        public void AddOrUpdate(ProjectViewModel projectViewModel)
        {
            // Get project entity from view  model
            var project = _mapper.Map<Project>(projectViewModel);

            if (projectViewModel.Id == 0)
                _projectRepo.Add(project);
            else
                _projectRepo.Update(project);
        }

        public ProjectViewModel Get(int id)
        {
            var projectViewModel = new ProjectViewModel();

            var project = _projectRepo.Get(id);

            if (project != null)
                projectViewModel = _mapper.Map<ProjectViewModel>(project);

            return projectViewModel;
        }

        public List<ProjectViewModel> GetProjects()
        {
            var projectModels = new List<ProjectViewModel>();

            // Projects are not expected to be more than 20
            var projects = _projectRepo.GetEntities(1, 50);

            foreach (var project in projects)
            {
                projectModels.Add(_mapper.Map<ProjectViewModel>(project));
            }

            return projectModels;
        }

        /// <summary>
        /// Return true/false if an project has given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Exists(int id)
        {
            return _projectRepo.ExistsById(id);
        }
    }
}
