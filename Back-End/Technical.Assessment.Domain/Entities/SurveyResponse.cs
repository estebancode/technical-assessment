using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical.Assessment.Domain.Entities
{

    [Serializable]
    [Table("SurveyResponse", Schema = "Responses")]
    public class SurveyResponse : BaseEntity<int>
    {
        public int SurveyId { get; set; }

        [ForeignKey("SurveyId")]
        public Survey Survey { get; set; }

        public int RespondentId { get; set; }

        [ForeignKey("RespondentId")]
        public Respondent Respondent { get; set; }
    }
}
