using System.ComponentModel.DataAnnotations;

namespace Technical.Assessment.Api.Dto
{
    public class UserDto
    {
        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
