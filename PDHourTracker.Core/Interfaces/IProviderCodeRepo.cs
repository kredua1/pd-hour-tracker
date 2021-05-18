using PDHourTracker.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IProviderCodeRepo<TEntity> : IBaseRepo<TEntity>
        where TEntity : class
    {
        /// <summary>
        /// Gets the current provider code based on current date.
        /// </summary>
        /// <returns></returns>
        ProviderCode GetCurrent();

        /// <summary>
        /// Gets provider code by given date range.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name=""></param>
        /// <param name=""></param>
        /// <returns></returns>
        ProviderCode GetByDates(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Return true/false if givenc code already exists.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        bool Exists(string code);
    }
}
