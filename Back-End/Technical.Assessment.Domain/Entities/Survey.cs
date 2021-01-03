using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical.Assessment.Domain.Entities
{

    [Serializable]
    [Table("Survey", Schema = "Surveys")]
    public class Survey : BaseEntity<int>
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        [Column(TypeName = "varchar(1000)")]
        public string Description { get; set; }
    }
}
