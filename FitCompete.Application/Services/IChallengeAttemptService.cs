using FitCompete.SharedKernel.Dtos;

namespace FitCompete.Application.Services
{
    public interface IChallengeAttemptService
    {
        Task<ChallengeAttemptResponseDto> AddAttemptAsync(ChallengeAttemptRequestDto attemptDto, int userId);
    }
}