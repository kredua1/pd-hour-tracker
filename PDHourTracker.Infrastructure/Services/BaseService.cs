using AutoMapper;
using PDHourTracker.Core.Interfaces;
using PDHourTracker.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Infrastructure.Services
{
    public abstract class BaseService<TDto, TEntity> : IBaseService<TDto, TEntity>
        where TDto : new()
        where TEntity : class
    {
        private const int _MAX_PAGE_SIZE = 50;

        protected AppDbContext _dbContext;
        //protected IBaseRepo<TEntity> _repo;
        protected IMapper _mapper;

        public BaseService(AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            //_repo = new BaseRepo<TEntity>(dbContext);
            _mapper = mapper;
        }

        /// <summary>
        /// Gets an entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(int id)
        {
            return _dbContext.Set<TEntity>()
                .Find(id);
        }

        /// <summary>
        /// Adds a new entity to the DB.
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TDto dto)
        {
            var entity = Activator.CreateInstance(typeof(TEntity)) as TEntity;
            _mapper.Map(dto, entity);
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Add multiple entities to the DB.
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(params TEntity[] entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Add multiple entities to the DB.
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _dbContext.Set<TEntity>().AddRange(entities);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Updates an entity in the DB>
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Removes entity for the given id from the DB.
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            var entity = Get(id);
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Gets a list of entities limited by page number and size.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<TEntity> GetEntities(int pageNum, int pageSize)
        {
            pageNum = ValidatePageNumber(pageNum);
            pageSize = ValidatePageSize(pageSize);

            return _dbContext.Set<TEntity>()
                .Skip((pageNum - 1) * pageSize)
                .Take(pageNum * pageSize)
                .ToList();
        }

        #region Helpers

        /// <summary>
        /// Ensures a positive number is returned for page number.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <returns></returns>
        private int ValidatePageNumber(int pageNum)
        {
            return pageNum > 0 ? pageNum : 1;
        }

        /// <summary>
        /// Ensures page size is positive and less than or equal to max page size.
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        private int ValidatePageSize(int pageSize)
        {
            return pageSize > 0 && pageSize <= _MAX_PAGE_SIZE ? pageSize : _MAX_PAGE_SIZE;
        }
        #endregion
    }
}
