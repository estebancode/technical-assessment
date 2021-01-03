using System;
using Technical.Assessment.Api.Dto;

namespace Technical.Assessment.IntegrationTest.Builder
{
    public class SurveyDtoBuilder
    {
        private readonly SurveyDto survey;

        public SurveyDtoBuilder()
        {
            survey = new SurveyDto
            {
                Name = $"{Guid.NewGuid()}-name",
                Description = $"{Guid.NewGuid()}-description"
            };
        }

        public SurveyDto Get()
        {
            return survey;
        }
    }
}
