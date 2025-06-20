using FitCompete.SharedKernel.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitCompete.Application.Services
{
    public interface IAchievementService
    {
        Task<IEnumerable<AchievementDto>> GetAllAchievementsAsync();
        Task<AchievementDto?> GetAchievementByIdAsync(int id);
        Task<AchievementDto> CreateAchievementAsync(AchievementCreateDto achievementDto);
        Task DeleteAchievementAsync(int id);
    }
}