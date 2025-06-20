using FitCompete.Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using FitCompete.SharedKernel.Dtos;
using FitCompete.Application.Services;
using FitCompete.Domain.Entities;
using FitCompete.Domain.Interfaces;



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
            _logger.LogInformation("User {UserName} is submitting an attempt for challenge {ChallengeId}", attemptDto.UserName, attemptDto.ChallengeId);

            try
            {
                var result = await _attemptService.AddAttemptAsync(attemptDto, 0); 
                if (result.EarnedAchievement != null)
                {
                    _logger.LogInformation("User {UserName} has earned a new achievement: {AchievementName}", attemptDto.UserName, result.EarnedAchievement.Name);
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
                _logger.LogError(ex, "An error occurred while posting an attempt for user");
                return StatusCode(500, "Wystąpił wewnętrzny błąd serwera.");
            }
        }

    }
}