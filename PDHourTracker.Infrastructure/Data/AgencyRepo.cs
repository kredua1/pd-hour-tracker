using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class AgencyRepo<TEntity> : BaseRepo<TEntity>, IAgencyRepo<TEntity>
        where TEntity : class
    {
        public AgencyRepo(AppDbContext dbContext)
            : base(dbContext) { }

        /// <summary>
        /// Returns true/false if an agency exists or not.
        /// </summary>
        /// <param name="agencyName"></param>
        /// <returns></returns>
        public bool Exists(string agencyName)
        {
            return _dbContext.Agencies.Any(a => a.AgencyName == agencyName);
        }

        /// <summary>
        /// Returns agency where AgencyName contains given search value.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Agency> Search(string searchValue)
        {
            return _dbContext.Agencies
                .Where(x => x.AgencyName.Contains(searchValue))
                .ToList();
        }
    }
}
