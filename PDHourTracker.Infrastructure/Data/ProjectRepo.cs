using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Data
{
    public class ProjectRepo<TEntity> : BaseRepo<TEntity>, IProjectRepo<TEntity>
        where TEntity : class
    {
        public ProjectRepo(AppDbContext dbContext)
            : base(dbContext) { }
    }
}
