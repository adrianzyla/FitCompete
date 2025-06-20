using FitCompete.Api.Controllers;
using FitCompete.Application.Services;
using FitCompete.SharedKernel.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FitCompete.Api.Controllers
{
    public class AchievementsController : BaseApiController
    {
        private readonly IAchievementService _achievementService;

        public AchievementsController(IAchievementService achievementService)
        {
            _achievementService = achievementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _achievementService.GetAllAchievementsAsync());

        [HttpPost]
        public async Task<IActionResult> Create(AchievementCreateDto dto)
        {
            var result = await _achievementService.CreateAchievementAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = result.AchievementId }, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _achievementService.DeleteAchievementAsync(id);
            return NoContent();
        }
    }
}