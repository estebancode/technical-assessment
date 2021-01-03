using System;
using System.Collections.Generic;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.UnitTest.Builder
{
    public class SurverBuilder
    {
        private readonly Survey survey;

        public SurverBuilder()
        {
            survey = new Survey
            {
                Name = $"{Guid.NewGuid()}-name",
                Description = $"{Guid.NewGuid()}-description"
            };
        }

        public Survey Get()
        {
            return survey;
        }

        public IEnumerable<Survey> Get_All()
        {
            return new List<Survey> { survey};
        }
    }
}
