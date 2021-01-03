using System.ComponentModel.DataAnnotations;

namespace Technical.Assessment.Api.Dto
{
    public class ResponseDto
    {
        [Required]
        public string Answer { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int RespondentId { get; set; }

        [Required]
        public int SurveyId { get; set; }
    }
}
