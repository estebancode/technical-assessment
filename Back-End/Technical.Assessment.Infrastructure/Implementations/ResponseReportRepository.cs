using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Technical.Assessment.Domain.Entities;
using Technical.Assessment.Domain.Interfaces;
using Technical.Assessment.Infrastructure.Context;
using Microsoft.Data.SqlClient;

namespace Technical.Assessment.Infrastructure.Implementations
{
    public class ResponseReportRepository : IResponseReportRepository
    {

        /// <summary>
        /// context's instance
        /// </summary>
        protected readonly TechnicalAssessmentContext context;

        public ResponseReportRepository(TechnicalAssessmentContext context)
        {
            this.context = context;
        }

        public IEnumerable<ResponseReport> GetAllBySurveyIdAndUser(int surveyId, int respondentId)
        {
            return this.context.ResponseReports.FromSqlRaw("[Responses].[GetAllResponsesBySurveyAndUser] @p0, @p1", parameters: new[]
            {
               new SqlParameter("@p0",surveyId),
               new SqlParameter("@p1",respondentId)
            });
        }
    }
}
