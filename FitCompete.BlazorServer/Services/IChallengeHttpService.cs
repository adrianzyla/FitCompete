using FitCompete.SharedKernel.Dtos;

namespace FitCompete.BlazorServer.Services
{
    public interface IChallengeHttpService
    {
        // Metody READ
        Task<IEnumerable<ChallengeDto>?> GetAllChallengesAsync();
        Task<ChallengeDto?> GetChallengeByIdAsync(int id);
        Task<IEnumerable<ChallengeCategoryDto>?> GetAllCategoriesAsync();

        // Metoda CREATE dla wyników
        Task<ChallengeAttemptResponseDto?> AddAttemptAsync(ChallengeAttemptRequestDto attempt);

        // --- Metody CREATE, UPDATE, DELETE dla Wyzwan ---
        Task<ChallengeDto?> CreateChallengeAsync(ChallengeCreateDto challenge);
        Task UpdateChallengeAsync(int challengeId, ChallengeUpdateDto challenge);
        Task DeleteChallengeAsync(int challengeId);

        // Metody dla osiągnięć
        Task<IEnumerable<AchievementDto>?> GetAllAchievementsAsync();
        Task<AchievementDto?> CreateAchievementAsync(AchievementCreateDto achievementDto);
        Task DeleteAchievementAsync(int id);
    }
}