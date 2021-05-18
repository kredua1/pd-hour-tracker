using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class SignOutSheetUploadRepo<TEntity> : BaseRepo<TEntity>, ISignOutSheetUploadRepo<TEntity>
        where TEntity : class
    {
        public SignOutSheetUploadRepo(AppDbContext dbContext)
            : base(dbContext) { }

        /// <summary>
        /// Get Id's for given workshop.
        /// </summary>
        /// <param name="workshopId"></param>
        /// <returns></returns>
        public List<int> GetIdsByWorkshop(int workshopId)
        {
            return _dbContext.SignOutSheetUploads
                .Where(s => s.WorkshopId == workshopId)
                .Select(s => s.Id)
                .ToList();
        }
    }
}
