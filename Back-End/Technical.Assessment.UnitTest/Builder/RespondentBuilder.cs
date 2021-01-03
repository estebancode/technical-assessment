using System;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.UnitTest.Builder
{
    public class RespondentBuilder
    {
        private readonly Respondent respondent;

        public RespondentBuilder()
        {
            respondent = new Respondent
            {
                Name = Guid.NewGuid().ToString(),
                HashedPassword = GenerateRandomPassword().ToString(),
                Email = $"{Guid.NewGuid()}@email.com"
            };
        }

        public Respondent Get()
        {
            return respondent;
        }

        private int GenerateRandomPassword()
        {
            int _min = 100000000;
            int _max = 999999999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}
