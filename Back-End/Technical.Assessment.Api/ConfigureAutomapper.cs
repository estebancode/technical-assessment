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
            CreateMap<Question, QuestionDto>()
                .ForPath(x => x.QuestionOrder.Order, opt => opt.MapFrom(x => x.Order))
                .ForPath(x => x.QuestionOrder.SurverId, opt => opt.MapFrom(x => x.SurverId))
                .ReverseMap();
            CreateMap<Response, ResponseDto>().ReverseMap();
        }
    }
}
