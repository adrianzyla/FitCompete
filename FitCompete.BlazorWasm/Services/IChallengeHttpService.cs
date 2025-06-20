using FitCompete.SharedKernel.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitCompete.BlazorWasm.Services
{
    public interface IChallengeHttpService
    {
        // --- Metody dla Wyzwań ---
        Task<IEnumerable<ChallengeDto>?> GetAllChallengesAsync();
        Task<ChallengeDto?> GetChallengeByIdAsync(int id);
        Task<IEnumerable<ChallengeCategoryDto>?> GetAllCategoriesAsync();
        Task<IEnumerable<RankingEntryDto>?> GetRankingAsync(int challengeId);
        Task<ChallengeDto?> CreateChallengeAsync(ChallengeCreateDto challenge);
        Task UpdateChallengeAsync(int challengeId, ChallengeUpdateDto challenge);
        Task DeleteChallengeAsync(int challengeId);

        // --- Metody dla Wyników ---
        Task<ChallengeAttemptResponseDto?> AddAttemptAsync(ChallengeAttemptRequestDto attempt);
        Task<RankingEntryDto?> GetAttemptByIdAsync(int attemptId); // NOWA METODA

        // --- Metody dla Osiągnięć ---
        Task<IEnumerable<AchievementDto>?> GetAllAchievementsAsync();
        Task<AchievementDto?> GetAchievementByIdAsync(int id);
        Task<AchievementDto?> CreateAchievementAsync(AchievementCreateDto achievementDto);
        Task UpdateAchievementAsync(int id, AchievementUpdateDto dto);
        Task DeleteAchievementAsync(int id);
    }
}