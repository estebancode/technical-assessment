using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical.Assessment.Domain.Entities
{

    [Serializable]
    [Table("Response", Schema = "Responses")]
    public class Response : DomainEntity
    {
        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string Answer { get; set; }

        [Key, ForeignKey("SurveyResponse")]
        [Column(Order = 1)]
        public int SurverResponseId { get; set; }

        [ForeignKey("SurverResponseId")]
        public SurveyResponse SurveyResponse { get; set; }

        [Key,ForeignKey("Question")]
        [Column(Order = 2)]
        public int QuestionId { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }

        [Key,ForeignKey("Respondent")]
        [Column(Order = 3)]
        public int RespondentId { get; set; }

        [ForeignKey("RespondentId")]
        public Respondent Respondent { get; set; }

        public int SurveyId { get; set; }
    }
}
