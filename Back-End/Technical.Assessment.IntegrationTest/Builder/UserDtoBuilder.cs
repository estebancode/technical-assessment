using Technical.Assessment.Api.Dto;

namespace Technical.Assessment.IntegrationTest.Builder
{
    public class UserDtoBuilder
    {
        private readonly UserDto userDto;

        public UserDtoBuilder()
        {
            userDto = new UserDto
            {
                Email = "tester.team@gmail.com",
                Password = "Team2021*"
            };
        }

        public UserDto Get()
        {
            return userDto;
        }
    }
}
