using AutoMapper;
using FitCompete.SharedKernel.Dtos;
using FitCompete.Domain.Entities;

namespace FitCompete.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ChallengeCategory, ChallengeCategoryDto>();
            CreateMap<Challenge, ChallengeDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.ChallengeCategory.Name))
                .ForMember(dest => dest.CreatedByUserName, opt => opt.MapFrom(src => src.CreatedByUser.UserName));

            CreateMap<Achievement, AchievementDto>();
            CreateMap<ChallengeAttempt, ChallengeAttemptResponseDto>();
            CreateMap<ChallengeCreateDto, Challenge>();
            CreateMap<ChallengeUpdateDto, Challenge>();
            CreateMap<AchievementCreateDto, Achievement>();
        }
    }
}