using System;
using System.ComponentModel.DataAnnotations;

namespace Technical.Assessment.Domain.Entities
{
    public class ResponseReport
    {
        [Key]
        public Guid Id { get; set; }
        public string SurveyName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int SurverResponseId { get; set; }
        public int QuestionId { get; set; }
        public int RespondentId { get; set; }
        public int SurveyId { get; set; }

    }
}
