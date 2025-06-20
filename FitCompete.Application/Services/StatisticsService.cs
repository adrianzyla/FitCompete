using FitCompete.SharedKernel.Dtos;
using FitCompete.Domain.Entities;
using FitCompete.Domain.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace FitCompete.Application.Services
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<DashboardStatsDto> GetDashboardStatsAsync()
        {
            var challenges = await _unitOfWork.Repository<Challenge>().GetAllAsync();
            var attempts = await _unitOfWork.Repository<ChallengeAttempt>().GetAllAsync();
            var users = await _unitOfWork.Repository<User>().GetAllAsync();

            var stats = new DashboardStatsDto
            {
                TotalChallenges = challenges.Count(),
                TotalAttempts = attempts.Count(),
                TotalUsers = users.Count()
            };

            return stats;
        }
    }
}