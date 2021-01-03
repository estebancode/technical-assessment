using System.ComponentModel.DataAnnotations;

namespace Technical.Assessment.Api.Dto
{
    public class SurveyDto
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}
