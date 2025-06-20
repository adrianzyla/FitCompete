using FitCompete.SharedKernel.Dtos;

namespace FitCompete.BlazorServer.Services
{
    public interface IChallengeHttpService
    {
        Task<IEnumerable<ChallengeDto>?> GetAllChallengesAsync();
        Task<ChallengeDto?> GetChallengeByIdAsync(int id);
        Task<IEnumerable<ChallengeCategoryDto>?> GetAllCategoriesAsync();

        Task<ChallengeAttemptResponseDto?> AddAttemptAsync(ChallengeAttemptRequestDto attempt);

        Task<ChallengeDto?> CreateChallengeAsync(ChallengeCreateDto challenge);
        Task UpdateChallengeAsync(int challengeId, ChallengeUpdateDto challenge);
        Task DeleteChallengeAsync(int challengeId);

        Task<IEnumerable<AchievementDto>?> GetAllAchievementsAsync();
        Task<AchievementDto?> GetAchievementByIdAsync(int id);
        Task<AchievementDto?> CreateAchievementAsync(AchievementCreateDto achievementDto);
        Task DeleteAchievementAsync(int id);
        Task UpdateAchievementAsync(int id, AchievementUpdateDto dto);
        Task<IEnumerable<RankingEntryDto>?> GetRankingAsync(int challengeId);
        Task<DashboardStatsDto?> GetDashboardStatsAsync();
    }
}