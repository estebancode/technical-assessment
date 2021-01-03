using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.Domain.Interfaces
{
    public interface IQuestionService
    {
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <param name="includeProperties"></param>
        /// <param name="isTracking"></param>
        /// <returns></returns>
        Task<IEnumerable<Question>> GetAllAsync(Expression<Func<Question, bool>> filter,
            Func<IQueryable<Question>, IOrderedQueryable<Question>> orderBy, string includeProperties,
            bool isTracking);

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Question> GetAsync(int id);

        /// <summary>
        /// Insert Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task InsertAsync(Question entity);

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task UpdateAsync(Question entity);

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Change order of the question
        /// </summary>
        /// <param name="SurveyId"></param>
        /// <param name="QuestionId"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<IEnumerable<QuestionOrder>> ChangeOrderAsync(int SurveyId, int QuestionId,int order);
    }
}
