using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IEmployeeRepo<TEntity> : IBaseRepo<TEntity>
        where TEntity : class
    {
        
    }
}
