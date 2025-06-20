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

        public async Task<ChallengeAttemptResponseDto> AddAttemptAsync(ChallengeAttemptRequestDto attemptDto, int userId)
        {
            // 1. Sprawdź, czy użytkownik i wyzwanie istnieją
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);
            var challenge = await _unitOfWork.Repository<Challenge>().GetByIdAsync(attemptDto.ChallengeId);

            if (user == null || challenge == null)
            {
                throw new KeyNotFoundException("Użytkownik lub wyzwanie nie istnieje.");
            }

            // 2. Stwórz i zapisz nowy wynik
            var newAttempt = new ChallengeAttempt
            {
                UserId = userId,
                ChallengeId = attemptDto.ChallengeId,
                ResultValue = attemptDto.ResultValue.Value,
                EvidenceUrl = attemptDto.EvidenceUrl,
                AttemptDate = DateTime.UtcNow
            };

            await _unitOfWork.Repository<ChallengeAttempt>().AddAsync(newAttempt);

            // 3. Sprawdź, czy za to wyzwanie jest osiągnięcie
            var achievement = (await _unitOfWork.Repository<Achievement>()
                                   .FindAsync(a => a.ChallengeId == challenge.ChallengeId))
                                   .FirstOrDefault();

            AchievementDto? earnedAchievementDto = null;
            if (achievement != null)
            {
                bool conditionMet = false;
                // Sprawdzamy, czy warunek został spełniony
                switch (achievement.Condition)
                {
                    case AchievementCondition.LessThan:
                        if (newAttempt.ResultValue < achievement.ThresholdValue)
                            conditionMet = true;
                        break;
                    case AchievementCondition.GreaterThan:
                        if (newAttempt.ResultValue > achievement.ThresholdValue)
                            conditionMet = true;
                        break;
                }

                // Jeśli warunek jest spełniony, sprawdzamy dalej
                if (conditionMet)
                {
                    // 4. Sprawdź, czy użytkownik już ma to osiągnięcie
                    var hasAchievement = (await _unitOfWork.Repository<UserAchievement>()
                                              .FindAsync(ua => ua.UserId == userId && ua.AchievementId == achievement.AchievementId))
                                              .Any();

                    if (!hasAchievement)
                    {
                        // 5. Jeśli nie ma, przyznaj je!
                        var newUserAchievement = new UserAchievement
                        {
                            UserId = userId,
                            AchievementId = achievement.AchievementId,
                            DateEarned = DateTime.UtcNow
                        };
                        await _unitOfWork.Repository<UserAchievement>().AddAsync(newUserAchievement);
                        earnedAchievementDto = _mapper.Map<AchievementDto>(achievement);
                    }
                }
            }

            // 6. Zapisz wszystkie zmiany w jednej transakcji
            await _unitOfWork.CompleteAsync();

            // 7. Zwróć odpowiedź
            var response = _mapper.Map<ChallengeAttemptResponseDto>(newAttempt);
            response.EarnedAchievement = earnedAchievementDto;

            return response;
        }
    }
}