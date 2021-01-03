using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.Domain.Interfaces
{
    public interface ISurveyService
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
            Task<IEnumerable<Survey>> GetAllAsync(Expression<Func<Survey, bool>> filter,
        Func<IQueryable<Survey>, IOrderedQueryable<Survey>> orderBy, string includeProperties,
        bool isTracking);

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Survey> GetAsync(int id);

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(Survey entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(Survey entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);
    }
}
