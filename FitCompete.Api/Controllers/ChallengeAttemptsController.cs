using FitCompete.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using FitCompete.SharedKernel.Dtos;
using FitCompete.Application.Services;



namespace FitCompete.Api.Controllers
{
    public class ChallengeAttemptsController : BaseApiController
    {
        private readonly IChallengeAttemptService _attemptService;
        private readonly ILogger<ChallengeAttemptsController> _logger;

        public ChallengeAttemptsController(IChallengeAttemptService attemptService, ILogger<ChallengeAttemptsController> logger)
        {
            _attemptService = attemptService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> PostAttempt(ChallengeAttemptRequestDto attemptDto)
        {
            // W prawdziwej aplikacji ID użytkownika wzięlibyśmy z tokena JWT po zalogowaniu.
            // Na razie dla testów użyjemy zahardkodowanego ID użytkownika 'Rocky' (ID=2)
            var userId = 2;

            _logger.LogInformation("User {UserId} is submitting an attempt for challenge {ChallengeId}", userId, attemptDto.ChallengeId);

            try
            {
                var result = await _attemptService.AddAttemptAsync(attemptDto, userId);
                if (result.EarnedAchievement != null)
                {
                    _logger.LogInformation("User {UserId} has earned a new achievement: {AchievementName}", userId, result.EarnedAchievement.Name);
                }
                return Ok(result);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while posting an attempt for user {UserId}", userId);
                return StatusCode(500, "Wystąpił wewnętrzny błąd serwera.");
            }
        }
    }
}