using System.Collections.Generic;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.Domain.Interfaces
{
    public interface IResponseReportRepository
    {
        IEnumerable<ResponseReport> GetAllBySurveyIdAndUser(int surveyId, int respondentId);
    }
}
