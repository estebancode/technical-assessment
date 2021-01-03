using System.ComponentModel.DataAnnotations;

namespace Technical.Assessment.Api.Dto
{
    public class UserDto
    {

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
