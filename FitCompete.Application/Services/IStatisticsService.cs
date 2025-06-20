using FitCompete.SharedKernel.Dtos;
using System.Threading.Tasks;

namespace FitCompete.Application.Services
{
    public interface IStatisticsService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}