using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;

namespace Technical.Assessment.Domain.Services
{
    public class QuestionService : IQuestionService, IDisposable
    {

        /// <summary>
        /// Question's instance
        /// </summary>
        private readonly IRepository<Question> repositoryQuestion;

        /// <summary>
        /// QuestionOrder's instance
        /// </summary>
        private readonly IRepository<QuestionOrder> repositoryQuestionOrder;

        const string QUESTION_DOES_NOT_EXIST = "Question does not exist";

        /// <summary>
        /// Constructor's method
        /// </summary>
        /// <param name="repositoryQuestion"></param>
        /// <param name="repositoryQuestionOrder"></param>
        public QuestionService(IRepository<Question> repositoryQuestion, IRepository<QuestionOrder> repositoryQuestionOrder)
        {
            this.repositoryQuestion = repositoryQuestion;
            this.repositoryQuestionOrder = repositoryQuestionOrder;
        }

        public async Task DeleteAsync(int id)
        {
            Question entity = await GetAsync(id);
            if (entity != null)
            {
                IEnumerable<QuestionOrder> questionOrders = await this.repositoryQuestionOrder.GetAllAsync(o=> o.QuestionId == entity.Id, null, null, true).ConfigureAwait(false);
                if (questionOrders.Any())
                {
                    repositoryQuestionOrder.Delete(questionOrders.FirstOrDefault());
                    await repositoryQuestionOrder.SaveChangesAsync().ConfigureAwait(false);
                    
                    repositoryQuestion.Delete(entity);
                    await this.repositoryQuestion.SaveChangesAsync().ConfigureAwait(false);
                }
                else
                {
                    throw new ArgumentNullException(QUESTION_DOES_NOT_EXIST);
                }
            }
            else
            {
                throw new ArgumentNullException(QUESTION_DOES_NOT_EXIST);
            }
        }

        public async Task<IEnumerable<Question>> GetAllAsync(Expression<Func<Question, bool>> filter, Func<IQueryable<Question>, IOrderedQueryable<Question>> orderBy, string includeProperties, bool isTracking)
        {
            return await this.repositoryQuestion.GetAllAsync(filter, orderBy, includeProperties, isTracking).ConfigureAwait(false);
        }

        public async Task<Question> GetAsync(int id)
        {
            return await this.repositoryQuestion.GetAsync(id).ConfigureAwait(false);
        }

        public async Task InsertAsync(Question entity)
        {
            this.repositoryQuestion.Insert(entity);
            await this.repositoryQuestion.SaveChangesAsync();
            this.repositoryQuestionOrder.Insert(new QuestionOrder {
                SurverId = entity.SurverId,
                Order = entity.Order,
                QuestionId = entity.Id
            });
            await this.repositoryQuestionOrder.SaveChangesAsync();
        }

        public async Task UpdateAsync(Question entity)
        {
            Question question = await GetAsync(entity.Id);
            if (question != null)
            {
                QuestionOrder questionOrder = (await this.repositoryQuestionOrder.GetAllAsync(o => o.QuestionId == entity.Id, null, null, true).ConfigureAwait(false)).FirstOrDefault();
                if (questionOrder != null)
                {
                    if (entity.Order != 0)
                    {
                        questionOrder.Order = entity.Order;
                        repositoryQuestionOrder.Update(questionOrder);
                        await repositoryQuestionOrder.SaveChangesAsync().ConfigureAwait(false);
                    }

                    question.Text = entity.Text;
                    repositoryQuestion.Update(question);
                    await this.repositoryQuestion.SaveChangesAsync().ConfigureAwait(false);

                } else
                {
                    throw new ArgumentNullException(QUESTION_DOES_NOT_EXIST);
                }
            }
            else
            {
                throw new ArgumentNullException(QUESTION_DOES_NOT_EXIST);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.repositoryQuestion.Dispose();
            this.repositoryQuestionOrder.Dispose();
        }
    }
}
