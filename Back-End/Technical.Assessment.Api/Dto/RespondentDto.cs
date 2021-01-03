using System.ComponentModel.DataAnnotations;

namespace Technical.Assessment.Api.Dto
{
    public class RespondentDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string HashedPassword { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(254)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
