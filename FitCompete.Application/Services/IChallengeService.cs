using FitCompete.SharedKernel.Dtos;

namespace FitCompete.Application.Services
{
    public interface IChallengeService
    {
        Task<IEnumerable<ChallengeDto>> GetAllChallengesAsync();
        Task<ChallengeDto?> GetChallengeByIdAsync(int id);
        Task<ChallengeDto> CreateChallengeAsync(ChallengeCreateDto challengeDto);
        Task UpdateChallengeAsync(int challengeId, ChallengeUpdateDto challengeDto);
        Task<bool> DeleteChallengeAsync(int challengeId);
        Task<IEnumerable<RankingEntryDto>> GetChallengeRankingAsync(int challengeId);
    }
}