
using Microsoft.EntityFrameworkCore;
using PDHourTracker.Core.Enums;
using PDHourTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PDHourTracker.Infrastructure.Data
{
    public class BaseRepo<TEntity> : IBaseRepo<TEntity>
        where TEntity : class
    {
        private const int _MAX_PAGE_SIZE = 50;

        protected AppDbContext _dbContext;

        public BaseRepo(AppDbContext dbContext)
        {
            _dbContext = dbContext;

            // Turn off tracking
            _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
        public TEntity Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            _dbContext.SaveChanges();

            return entity;
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

        /// <summary>
        /// Gets a list of entities ordered by given predicate.
        /// </summary>
        /// <param name="order">The column to order by.</param>
        /// <param name="sort">Sort direction: ASC or DESC</param>
        /// <param name="pageNum">Page number to select</param>
        /// <param name="pageSize">Number of rows to select</param>
        /// <returns></returns>
        public List<TEntity> GetEntities(Func<TEntity, object> order, Sorted sort, int pageNum, int pageSize)
        {
            var query = sort == Sorted.ASC ?
                _dbContext.Set<TEntity>()
                    .OrderBy(order)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                :
                _dbContext.Set<TEntity>()
                    .OrderByDescending(order)
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize);

            return query.ToList();
        }

        /// <summary>
        /// Returns the total number of entities.
        /// </summary>
        /// <returns></returns>
        public int Total()
        {
            return _dbContext.Set<TEntity>().Count();
        }

        public bool ExistsById(int id)
        {
            var entity = _dbContext.Set<TEntity>().Find(id);

            // Return true/false if entity exists (was found)
            return entity != null;
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
