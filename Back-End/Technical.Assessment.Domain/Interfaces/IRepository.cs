using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.Domain.Interfaces
{

    /// <summary>
    /// interface IRepository
    /// </summary>
    /// <typeparam name="E">Entity</typeparam>
    /// <typeparam name="T">Type</typeparam>
    public interface IRepository<E> : IDisposable where E : DomainEntity
    {
        Task<IEnumerable<E>> GetAllAsync(Expression<Func<E, bool>> filter,
            Func<IQueryable<E>, IOrderedQueryable<E>> orderBy, string includeProperties,
            bool isTracking);

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<E> GetAsync(object id);

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<E> GetByAsync(Expression<Func<E, bool>> expression);

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(E entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(E entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(E entity);

        /// <summary>
        /// add range Entity
        /// </summary>
        /// <param name="entitys"></param>
        void AddRange(IEnumerable<E> entitys);

        /// <summary>
        /// Save Changes async
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();

        /// <summary>
        /// update range Entity
        /// </summary>
        /// <param name="entitys"></param>
        void UpdateRange(IEnumerable<E> entitys);

        /// <summary>
        /// Save Changes
        /// </summary>
        void SaveChanges();
    }
}
