using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.Domain.Interfaces
{
    public interface IBaseService<TEntity, T> where TEntity : BaseEntity<T>
    {
        Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties,
            bool isTracking, bool UseCache);
        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetAsync(T id);
        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);
        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);
        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> DeleteAsync(T id);
    }
}
