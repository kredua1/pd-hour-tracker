using Microsoft.EntityFrameworkCore;
using PDHourTracker.Core.Dtos;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class WorkshopRepo<TEntity> : BaseRepo<TEntity>, IWorkshopRepo<TEntity>
        where TEntity : class
    {
        public WorkshopRepo(AppDbContext dbContext)
            :base(dbContext)
        {

        }

        /// <summary>
        /// Gets all workshops for given employee/contact.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public List<Workshop> ByEmployee(int employeeId)
        {
            return _dbContext.Workshops
                .Where(w => w.EmployeeId == employeeId)
                .OrderBy(w => w.WorkshopName)
                .ToList();
        }

        /// <summary>
        /// Gets all workshops for given project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public List<Workshop> ByProject(int projectId)
        {
            return _dbContext.Workshops
                .Where(w => w.ProjectId == projectId)
                .OrderBy(w => w.WorkshopName)
                .ToList();
        }

        /// <summary>
        /// Gets workshop along with all related entities: Project, Employee, Provider Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Workshop GetDetails(int id)
        {
            return _dbContext.Workshops
                .Where(w => w.Id == id)
                .Include(w => w.Project)
                .Include(w => w.Employee)
                .Include(w => w.ProviderCode)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets all workshops with hours for given attendee Id.
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <returns>List of Workshops with AttendeeHours.</returns>
        //public List<Workshop> GetWorkshopsByAttendee(int attendeeId)
        //{
        //    return (from ah in _dbContext.AttendeeHours
        //            where ah.AttendeeId == attendeeId
        //            join w in _dbContext.Workshops
        //            on ah.WorkshopId equals w.Id
        //            select new Workshop
        //            {
        //                Id = w.Id,
        //                WorkshopName = w.WorkshopName,
        //                AttendeeHours = new List<AttendeeHour>
        //                {
        //                    new AttendeeHour
        //                    {
        //                        PDHours = ah.PDHours
        //                    }
        //                }
        //            })
        //            .ToList();
        //}

        /// <summary>
        /// Gets all workshops with hours for given attendee Id.
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <returns>List of Workshops with AttendeeHours.</returns>
        //public List<AttendeeWorkshopHour> GetAttendeeWorkshopHours(int attendeeId)
        //{
        //    return (from ah in _dbContext.AttendeeHours
        //            where ah.AttendeeId == attendeeId
        //            join w in _dbContext.Workshops
        //            on ah.WorkshopId equals w.Id
        //            select new AttendeeWorkshopHour
        //            {
        //                WorkshopId = w.Id,
        //                WorkshopName = w.WorkshopName,
        //                TrainingDate = w.TrainingDate,
        //                PDHours = ah.PDHours
        //            })
        //            .ToList();
        //}

        public List<Workshop> Search(string workshopName, int projectId, 
            DateTime? startDate = null, DateTime? endDate = null)
        {
            // Filter by project name
            var searchQuery = _dbContext.Workshops
                .Where(w => w.WorkshopName.Contains(workshopName));

            // Filter by project Id if given
            if(projectId > 0)
            {
                searchQuery = searchQuery.Where(w => w.ProjectId == projectId);
            }

            // Filter by training dates >= date if given
            if(startDate != null)
            {
                DateTime sDate = Convert.ToDateTime(startDate);
                searchQuery = searchQuery.Where(w => w.TrainingDate >= sDate);
            }

            // Filter by trainings dates <= date if given
            if(endDate != null)
            {
                DateTime eDate = Convert.ToDateTime(endDate);
                searchQuery = searchQuery.Where(w => w.TrainingDate <= eDate);
            }

            return searchQuery.ToList();
        }

        /// <summary>
        /// Returns true if given session identifier is not assigned to a workshop
        /// </summary>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        public bool SessionIdentifierIsUnique(string sessionIdentifier)
        {
            return !_dbContext.Workshops
                .Any(w => w.SessionIdentifier == sessionIdentifier);
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
            return !_dbContext.Workshops
                .Any(w => w.SessionIdentifier == sessionIdentifier
                    && w.Id != workshopId);
        }

        /// <summary>
        /// Search for workshops by name. Returns all workshops with names
        /// containing the given search value.
        /// </summary>
        /// <param name="workshopName"></param>
        /// <returns></returns>
        public List<Workshop> Search(string searchValue, int pageNumber, int pageSize)
        {
            return _dbContext.Workshops
                .Where(w => w.WorkshopName.Contains(searchValue))
                .OrderBy(w => w.WorkshopName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
