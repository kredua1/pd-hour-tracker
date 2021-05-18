using PDHourTracker.Core.Entities;
using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class ProviderCodeRepo<TEntity> : BaseRepo<TEntity>, IProviderCodeRepo<TEntity>
        where TEntity : class
    {
        public ProviderCodeRepo(AppDbContext dbContext)
            : base(dbContext) { }

        /// <summary>
        /// Return true/false if given code already exists.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool Exists(string code)
        {
            return _dbContext.ProviderCodes.Any(c => c.Code == code);
        }

        /// <summary>
        /// Gets provider code by given date range.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        public ProviderCode GetByDates(DateTime startDate, DateTime endDate)
        {
            return _dbContext.ProviderCodes
                .FirstOrDefault(pc => pc.StartDate.Date <= startDate.Date
                    && pc.EndDate.Date >= endDate.Date);
        }

        /// <summary>
        /// Gets the current provider code based on current date.
        /// </summary>
        /// <returns></returns>
        public ProviderCode GetCurrent()
        {
            return _dbContext.ProviderCodes
                .FirstOrDefault(pc => pc.StartDate.Date <= DateTime.Now.Date
                    && pc.EndDate.Date >= DateTime.Now.Date);
        }
    }
}
