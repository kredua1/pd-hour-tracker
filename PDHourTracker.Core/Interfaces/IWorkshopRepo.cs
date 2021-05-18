using PDHourTracker.Core.Dtos;
using PDHourTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IWorkshopRepo<TEntity> : IBaseRepo<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets all workshops for given attendee Id.
        /// </summary>
        /// <param name="attendeeId"></param>
        /// <returns></returns>
        //List<AttendeeWorkshopHour> GetAttendeeWorkshopHours(int attendeeId);

        /// <summary>
        /// Gets all workshops for given employee/contact.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        List<Workshop> ByEmployee(int employeeId);

        /// <summary>
        /// Gets all workshops for given project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        List<Workshop> ByProject(int projectId);

        /// <summary>
        /// Returns true if session identifier doesn't exist.
        /// </summary>
        /// <param name="sessionIdentifier"></param>
        /// <returns></returns>
        bool SessionIdentifierIsUnique(string sessionIdentifier);

        /// <summary>
        /// Returns true if session identifier doesn't exist on
        /// workshops other than the one the workshopId is given for.
        /// </summary>
        /// <param name="sessionIdentifier"></param>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        bool SessionIdentifierIsUnique(string sessionIdentifier, int workshopId);

        /// <summary>
        /// Gets workshop along with all related entities: Project, Employee, Provider Code
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Workshop GetDetails(int id);

        /// <summary>
        /// Search for workshops by name. Returns all workshops with names
        /// containing the given search value.
        /// </summary>
        /// <param name="workshopName"></param>
        /// <returns></returns>
        List<Workshop> Search(string searchValue, int pageNumber, int pageSize);
    }
}
