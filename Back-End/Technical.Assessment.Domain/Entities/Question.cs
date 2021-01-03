using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical.Assessment.Domain.Entities
{
    [Serializable]
    [Table("Question", Schema = "Surveys")]
    public class Question : BaseEntity<int>
    {
        [MaxLength(200)]
        [Column(TypeName = "varchar(200)")]
        public string Text { get; set; }

        public int Order { get; set; }

        public int SurverId { get; set; }
    }
}
