using FitCompete.SharedKernel.Dtos;

namespace FitCompete.BlazorWasm.Services
{
    public interface IChallengeHttpService
    {
        Task<IEnumerable<ChallengeDto>?> GetAllChallengesAsync();
        Task<ChallengeDto?> GetChallengeByIdAsync(int id);
        Task<ChallengeAttemptResponseDto?> AddAttemptAsync(ChallengeAttemptRequestDto attempt);
    }
}