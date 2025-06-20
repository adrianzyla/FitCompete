using FitCompete.Api.Controllers;
using FitCompete.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FitCompete.Api.Controllers
{
    public class StatisticsController : BaseApiController
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboardStats()
        {
            var stats = await _statisticsService.GetDashboardStatsAsync();
            return Ok(stats);
        }
    }
}