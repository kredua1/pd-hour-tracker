using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface ISignOutSheetUploadRepo<TEntity> : IBaseRepo<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Get Id's for given workshop
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        List<int> GetIdsByWorkshop(int workshopId);
    }
}
