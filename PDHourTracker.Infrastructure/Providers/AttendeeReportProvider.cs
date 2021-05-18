using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using PDHourTracker.Core.Dtos;
using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Providers
{
    public class AttendeeReportProvider : IAttendeeReportProvider
    {
        private AppDbContext _dbContext;

        public AttendeeReportProvider(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets attendees with agency name included
        /// ordered by last name and then first name.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<AttendeeWithAgency> AttendeeWithAgencies(int pageNum, int pageSize, string searchValue = null)
        {
            // Setup the query to join attendees and agencies and select 
            // a new object: AttendeeWithAgency
            var query = from a in _dbContext.Attendees
                        join ag in _dbContext.Agencies
                        on a.AgencyId equals ag.Id
                        select new AttendeeWithAgency
                        {
                            Id = a.Id,
                            FirstName = a.FirstName,
                            LastName = a.LastName,
                            MiddleName = a.MiddleName,
                            JobTitle = a.JobTitle,
                            AgencyId = a.AgencyId,
                            AgencyName = ag.AgencyName,
                            CertId = a.CertId
                        };

            // If a search value is given, filter on it
            if (!string.IsNullOrEmpty(searchValue))
            {
                query = query.Where(x => x.FirstName.Contains(searchValue)
                                    || x.LastName.Contains(searchValue)
                                    || x.MiddleName.Contains(searchValue));
            }

            // Return the ordered results paged
            return query.OrderBy(a => a.LastName)
                        .ThenBy(a => a.FirstName)
                        .Skip((pageNum - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();
        }
    }
}
