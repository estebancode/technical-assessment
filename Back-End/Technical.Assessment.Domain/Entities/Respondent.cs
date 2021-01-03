using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Technical.Assessment.Domain.Entities
{

    [Serializable]
    [Table("Respondent", Schema = "Users")]
    public class Respondent : BaseEntity<int>
    {
        [MaxLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "varchar(100)")]
        public string HashedPassword { get; set; }

        [MaxLength(254)]
        [Column(TypeName = "varchar(254)")]
        public string Email { get; set; }
    }
}
