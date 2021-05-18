using PDHourTracker.Web.Models.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Web.Services.Interfaces
{
    public interface IProjectService
    {
        ProjectViewModel Get(int id);
        List<ProjectViewModel> GetProjects();
        void AddOrUpdate(ProjectViewModel projectViewModel);
        bool Exists(int id);
    }
}
