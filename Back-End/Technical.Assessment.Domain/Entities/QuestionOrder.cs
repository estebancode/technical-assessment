using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical.Assessment.Domain.Entities
{
    [Serializable]
    [Table("QuestionOrder", Schema = "Surveys")]
    public class QuestionOrder : DomainEntity
    {
        public int Order { get; set; }

        [Key,ForeignKey("Survey")]
        [Column(Order = 1)]
        public int SurverId { get; set; }

        [ForeignKey("SurverId")]
        public Survey Survey { get; set; }

        [Key,ForeignKey("Question")]
        [Column(Order = 2)]
        public int QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
    }
}
