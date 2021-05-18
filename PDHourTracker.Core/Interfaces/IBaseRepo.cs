using PDHourTracker.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDHourTracker.Core.Interfaces
{
    public interface IBaseRepo<TEntity> where TEntity : class
    {
        /// <summary>
        /// Gets an entity by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Get(int id);

        /// <summary>
        /// Adds a new entity to the DB.
        /// </summary>
        /// <param name="entity"></param>
        TEntity Add(TEntity entity);

        /// <summary>
        /// Add multiple entities to the DB.
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(params TEntity[] entities);

        /// <summary>
        /// Add multiple entities to the DB.
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Updates an entity in the DB>
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);

        /// <summary>
        /// Removes entity for the given id from the DB.
        /// </summary>
        /// <param name="id"></param>
        void Delete(int id);

        /// <summary>
        /// Gets a list of entities limited by page number and size.
        /// </summary>
        /// <param name="pageNum"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        List<TEntity> GetEntities(int pageNum, int pageSize);

        /// <summary>
        /// Gets a list of entities ordered by given predicate.
        /// </summary>
        /// <param name="order">The column to order by.</param>
        /// <param name="sort">Sort direction: ASC or DESC</param>
        /// <param name="pageNum">Page number to select</param>
        /// <param name="pageSize">Number of rows to select</param>
        /// <returns></returns>
        List<TEntity> GetEntities(Func<TEntity, object> order, Sorted sort, int pageNum, int pageSize);

        /// <summary>
        /// Returns the total number of entities.
        /// </summary>
        /// <returns></returns>
        int Total();

        /// <summary>
        /// Returns true/false if entity has given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool ExistsById(int id);
    }
}
