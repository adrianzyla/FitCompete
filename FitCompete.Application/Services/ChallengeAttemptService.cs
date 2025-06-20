using AutoMapper;
using FitCompete.SharedKernel.Dtos;
using FitCompete.Domain.Entities;
using FitCompete.Domain.Interfaces;
using FitCompete.SharedKernel.Enums;


namespace FitCompete.Application.Services
{
    public class ChallengeAttemptService : IChallengeAttemptService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChallengeAttemptService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ChallengeAttemptResponseDto> AddAttemptAsync(ChallengeAttemptRequestDto attemptDto, int fallbackUserId)
        {
            var challenge = await _unitOfWork.Repository<Challenge>().GetByIdAsync(attemptDto.ChallengeId);
            if (challenge == null)
            {
                throw new KeyNotFoundException("Wyzwanie o podanym ID nie istnieje.");
            }

            //Znajdź użytkownika po nazwie. Jeśli nie istnieje, stwórz nowego.
            User? user = (await _unitOfWork.Repository<User>().FindAsync(u => u.UserName == attemptDto.UserName)).FirstOrDefault();

            if (user == null)
            {
                user = new User
                {
                    UserName = attemptDto.UserName,
                    Email = $"{attemptDto.UserName.ToLower()}@guest.fitcompete.com", 
                    HashedPassword = "guest_password_placeholder",
                    RegistrationDate = DateTime.UtcNow,
                    IsAdmin = false
                };
                await _unitOfWork.Repository<User>().AddAsync(user);
                await _unitOfWork.CompleteAsync();
            }

            var newAttempt = new ChallengeAttempt
            {
                UserId = user.UserId,
                ChallengeId = attemptDto.ChallengeId,
                ResultValue = attemptDto.ResultValue.Value,
                EvidenceUrl = attemptDto.EvidenceUrl,
                AttemptDate = DateTime.UtcNow
            };
            await _unitOfWork.Repository<ChallengeAttempt>().AddAsync(newAttempt);

            var achievement = (await _unitOfWork.Repository<Achievement>().FindAsync(a => a.ChallengeId == challenge.ChallengeId)).FirstOrDefault();
            AchievementDto? earnedAchievementDto = null;
            if (achievement != null)
            {
                bool conditionMet = false;
                switch (achievement.Condition)
                {
                    case AchievementCondition.LessThan:
                        if (newAttempt.ResultValue < achievement.ThresholdValue) conditionMet = true;
                        break;
                    case AchievementCondition.GreaterThan:
                        if (newAttempt.ResultValue > achievement.ThresholdValue) conditionMet = true;
                        break;
                }
                if (conditionMet)
                {
                    var hasAchievement = (await _unitOfWork.Repository<UserAchievement>().FindAsync(ua => ua.UserId == user.UserId && ua.AchievementId == achievement.AchievementId)).Any();
                    if (!hasAchievement)
                    {
                        var newUserAchievement = new UserAchievement { UserId = user.UserId, AchievementId = achievement.AchievementId, DateEarned = DateTime.UtcNow };
                        await _unitOfWork.Repository<UserAchievement>().AddAsync(newUserAchievement);
                        earnedAchievementDto = _mapper.Map<AchievementDto>(achievement);
                    }
                }
            }

            await _unitOfWork.CompleteAsync();

            var response = _mapper.Map<ChallengeAttemptResponseDto>(newAttempt);
            response.EarnedAchievement = earnedAchievementDto;
            return response;
        }

        public async Task<bool> DeleteAttemptAsync(int attemptId)
        {
            var attempt = await _unitOfWork.Repository<ChallengeAttempt>().GetByIdAsync(attemptId);
            if (attempt == null)
            {
                return false; 
            }

            _unitOfWork.Repository<ChallengeAttempt>().Remove(attempt);
            await _unitOfWork.CompleteAsync();
            return true; 
        }
    }
}
