using PDHourTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IAgencyRepo<TEntity> : IBaseRepo<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Returns agency where AgencyName contains given search value.
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Agency> Search(string searchValue);

        bool Exists(string agencyName);
    }
}
