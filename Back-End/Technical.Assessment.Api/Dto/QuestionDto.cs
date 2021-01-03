using System.ComponentModel.DataAnnotations;

namespace Technical.Assessment.Api.Dto
{
    public class QuestionDto
    {
        [MaxLength(200)]
        public string Text { get; set; }

        public QuestionOrderDto QuestionOrder { get; set; }
    }
}
