using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;

namespace Technical.Assessment.Domain.Services
{
    public class ResponseService : IResponseService, IDisposable
    {
        /// <summary>
        /// Response's instance
        /// </summary>
        private readonly IRepository<Response> repositoryResponse;

        /// <summary>
        /// SurveyResponse's instance
        /// </summary>
        private readonly IRepository<SurveyResponse> repositorySurveyResponse;

        /// <summary>
        /// response report's instance
        /// </summary>
        private readonly IResponseReportRepository repositoryResponseReport;

        const string RESPONSE_DOES_NOT_EXIST = "Response does not exist";

        /// <summary>
        /// Constructor's method
        /// </summary>
        /// <param name="repositoryResponse"></param>
        /// <param name="repositorySurveyResponse"></param>
        public ResponseService(IRepository<Response> repositoryResponse,
            IRepository<SurveyResponse> repositorySurveyResponse,
            IResponseReportRepository repositoryResponseReport)
        {
            this.repositoryResponseReport = repositoryResponseReport;
            this.repositoryResponse = repositoryResponse;
            this.repositorySurveyResponse = repositorySurveyResponse;
        }

        public async Task DeleteAsync(int id)
        {
            Response entity = await GetAsync(id);
            if (entity != null)
            {
                IEnumerable<SurveyResponse> surveyResponses = await this.repositorySurveyResponse
                    .GetAllAsync(o => o.Id == entity.SurverResponseId, null, null, true).ConfigureAwait(false);
                if (surveyResponses.Any())
                {
                    repositorySurveyResponse.Delete(surveyResponses.FirstOrDefault());
                    await repositorySurveyResponse.SaveChangesAsync().ConfigureAwait(false);

                    repositoryResponse.Delete(entity);
                    await this.repositoryResponse.SaveChangesAsync().ConfigureAwait(false);
                }
                else
                {
                    throw new ArgumentNullException(RESPONSE_DOES_NOT_EXIST);
                }
            }
            else
            {
                throw new ArgumentNullException(RESPONSE_DOES_NOT_EXIST);
            }
        }

        public async Task<IEnumerable<Response>> GetAllAsync(Expression<Func<Response, bool>> filter, Func<IQueryable<Response>, IOrderedQueryable<Response>> orderBy, string includeProperties, bool isTracking)
        {
            return await this.repositoryResponse.GetAllAsync(filter, orderBy, includeProperties, isTracking).ConfigureAwait(false);
        }

        public async Task<Response> GetAsync(int id)
        {
            return await this.repositoryResponse.GetAsync(id).ConfigureAwait(false);
        }

        public async Task InsertAsync(Response entity)
        {
            SurveyResponse surveyResponse = new SurveyResponse
            {
                SurveyId = entity.SurveyId,
                RespondentId = entity.RespondentId,
            };
            this.repositorySurveyResponse.Insert(surveyResponse);
            await this.repositorySurveyResponse.SaveChangesAsync();

            entity.SurverResponseId = surveyResponse.Id;

            this.repositoryResponse.Insert(entity);
            await this.repositoryResponse.SaveChangesAsync();
        }

        public async Task UpdateAsync(Response entity)
        {
            Response response = (await GetAllAsync(r=> r.QuestionId == entity.QuestionId && r.RespondentId == entity.RespondentId,null,null,false)).FirstOrDefault();
            if (response != null)
            {
                response.Answer = entity.Answer;
                repositoryResponse.Update(response);
                await this.repositoryResponse.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                throw new ArgumentNullException(RESPONSE_DOES_NOT_EXIST);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            this.repositoryResponse.Dispose();
            this.repositorySurveyResponse.Dispose();
        }

        public IEnumerable<ResponseReport> GetAllBySurveyIdAndUser(int surveyId, int respondentId)
        {
            return this.repositoryResponseReport.GetAllBySurveyIdAndUser(surveyId, respondentId);
        }
    }
}
