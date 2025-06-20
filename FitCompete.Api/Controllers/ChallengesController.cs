using FitCompete.Api.Controllers;
using FitCompete.Application.Services;
using FitCompete.SharedKernel.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FitCompete.Api.Controllers
{
    public class ChallengesController : BaseApiController
    {
        private readonly IChallengeService _challengeService;
        private readonly ILogger<ChallengesController> _logger;

        public ChallengesController(IChallengeService challengeService, ILogger<ChallengesController> logger)
        {
            _challengeService = challengeService;
            _logger = logger;
        }

        // GET /api/challenges
        [HttpGet]
        public async Task<IActionResult> GetAllChallenges()
        {
            _logger.LogInformation("API Endpoint: Getting all challenges.");
            var challenges = await _challengeService.GetAllChallengesAsync();
            return Ok(challenges);
        }

        // GET /api/challenges/{id} 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChallengeById(int id)
        {
            _logger.LogInformation("API Endpoint: Getting challenge with ID: {ChallengeId}", id);
            var challenge = await _challengeService.GetChallengeByIdAsync(id);

            if (challenge == null)
            {
                return NotFound();
            }

            return Ok(challenge);
        }

        // POST /api/challenges
        [HttpPost]
        public async Task<IActionResult> CreateChallenge([FromBody] ChallengeCreateDto challengeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var createdChallenge = await _challengeService.CreateChallengeAsync(challengeDto);
            // Zwracamy kod 201 Created z linkiem do nowo utworzonego zasobu
            return CreatedAtAction(nameof(GetChallengeById), new { id = createdChallenge.ChallengeId }, createdChallenge);
        }

        // PUT /api/challenges/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateChallenge(int id, [FromBody] ChallengeUpdateDto challengeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _challengeService.UpdateChallengeAsync(id, challengeDto);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            // Zwracamy kod 204 No Content, co jest standardem dla udanych operacji PUT
            return NoContent();
        }

        // DELETE /api/challenges/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChallenge(int id)
        {
            var success = await _challengeService.DeleteChallengeAsync(id);
            if (!success)
            {
                return NotFound();
            }
            // Zwracamy kod 204 No Content, co jest standardem dla udanych operacji DELETE
            return NoContent();
        }
    }
}