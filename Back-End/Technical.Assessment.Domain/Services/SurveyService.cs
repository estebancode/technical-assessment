using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;

namespace Technical.Assessment.Domain.Services
{
    public class SurveyService : ISurveyService, IDisposable
    {

        /// <summary>
        /// Repository's instance
        /// </summary>
        private readonly IRepository<Survey> repository;

        const string DOES_NOT_EXIST_SURVEY = "Survey does not exist";

        public SurveyService(IRepository<Survey> repository)
        {
            this.repository = repository;
        }

        public async Task DeleteAsync(int id)
        {
            Survey survey = await GetAsync(id);
            if (survey != null)
            {
                repository.Delete(survey);
                await this.repository.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentNullException(DOES_NOT_EXIST_SURVEY);
            }
        }

        public async Task<IEnumerable<Survey>> GetAllAsync(Expression<Func<Survey, bool>> filter, Func<IQueryable<Survey>, IOrderedQueryable<Survey>> orderBy, string includeProperties, bool isTracking)
        {
            return await this.repository.GetAllAsync(filter, orderBy, includeProperties, isTracking).ConfigureAwait(false);
        }

        public async Task<Survey> GetAsync(int id)
        {
            return await this.repository.GetAsync(id).ConfigureAwait(false);
        }

        public async Task InsertAsync(Survey entity)
        {
            this.repository.Insert(entity);
            await this.repository.SaveChangesAsync();
        }

        public async Task UpdateAsync(Survey entity)
        {
            Survey survey = await GetAsync(entity.Id);
            if (survey != null)
            {
                survey.Name = entity.Name;
                survey.Description = entity.Description;
                repository.Update(survey);
                await this.repository.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentNullException(DOES_NOT_EXIST_SURVEY);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.repository.Dispose();
        }
    }
}
