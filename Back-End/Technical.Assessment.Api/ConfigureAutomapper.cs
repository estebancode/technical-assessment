using AutoMapper;
using Technical.Assessment.Api.Dto;
using Technical.Assessment.Domain.Entities;

namespace Technical.Assessment.Api
{
    public class ConfigureAutomapper: Profile
    {
        public ConfigureAutomapper()
        {
            CreateMap<Respondent, RespondentDto>().ReverseMap();
            CreateMap<Respondent, UserDto>()
                .ForMember(x => x.Password, opt => opt.MapFrom(x=> x.HashedPassword))
                .ReverseMap();
            CreateMap<Survey, SurveyDto>().ReverseMap();
            CreateMap<Question, QuestionDto>().ReverseMap();
        }
    }
}
