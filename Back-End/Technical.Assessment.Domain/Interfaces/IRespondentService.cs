using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.Domain.Interfaces
{
    public interface IRespondentService
    {

        Task<IEnumerable<Respondent>> GetAllAsync(Expression<Func<Respondent, bool>> filter,
            Func<IQueryable<Respondent>, IOrderedQueryable<Respondent>> orderBy, string includeProperties,
            bool isTracking);
        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Respondent> GetAsync(int id);

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(Respondent entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(Respondent entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(string email);

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<string> AuthenticaUserAsync(Respondent entity);
    }
}
