using FitCompete.SharedKernel.Dtos;

namespace FitCompete.BlazorWasm.Services
{
    public interface IChallengeHttpService
    {
        // Metody READ
        Task<IEnumerable<ChallengeDto>?> GetAllChallengesAsync();
        Task<ChallengeDto?> GetChallengeByIdAsync(int id);
        Task<IEnumerable<ChallengeCategoryDto>?> GetAllCategoriesAsync();

        // Metoda dla wyników
        Task<ChallengeAttemptResponseDto?> AddAttemptAsync(ChallengeAttemptRequestDto attempt);

        // Metody dla wyzwan
        Task<ChallengeDto?> CreateChallengeAsync(ChallengeCreateDto challenge);
        Task UpdateChallengeAsync(int challengeId, ChallengeUpdateDto challenge);
        Task DeleteChallengeAsync(int challengeId);

        // Metody dla osiągnięć
        Task<IEnumerable<AchievementDto>?> GetAllAchievementsAsync();
        Task<AchievementDto?> GetAchievementByIdAsync(int id);
        Task<AchievementDto?> CreateAchievementAsync(AchievementCreateDto achievementDto);
        Task DeleteAchievementAsync(int id);
        Task UpdateAchievementAsync(int id, AchievementUpdateDto dto);

    }
}