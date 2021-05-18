using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class EmployeeRepo<TEntity> : BaseRepo<TEntity>, IEmployeeRepo<TEntity>
        where TEntity : class
    {
        public EmployeeRepo(AppDbContext dbContext)
            : base(dbContext) { }
    }
}
