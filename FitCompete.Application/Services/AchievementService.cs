using AutoMapper;
using FitCompete.Domain.Entities;
using FitCompete.Domain.Interfaces;
using FitCompete.SharedKernel.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FitCompete.Application.Services
{
    public class AchievementService : IAchievementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AchievementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AchievementDto> CreateAchievementAsync(AchievementCreateDto achievementDto)
        {
            var achievement = _mapper.Map<Achievement>(achievementDto);
            await _unitOfWork.Repository<Achievement>().AddAsync(achievement);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<AchievementDto>(achievement);
        }

        public async Task DeleteAchievementAsync(int id)
        {
            var achievement = await _unitOfWork.Repository<Achievement>().GetByIdAsync(id);
            if (achievement != null)
            {
                _unitOfWork.Repository<Achievement>().Remove(achievement);
                await _unitOfWork.CompleteAsync();
            }
        }

        public async Task<IEnumerable<AchievementDto>> GetAllAchievementsAsync()
        {
            var achievements = await _unitOfWork.Repository<Achievement>().GetAllAsync();
            return _mapper.Map<IEnumerable<AchievementDto>>(achievements);
        }

        public async Task<AchievementDto?> GetAchievementByIdAsync(int id)
        {
            var achievement = await _unitOfWork.Repository<Achievement>().GetByIdAsync(id);
            return _mapper.Map<AchievementDto>(achievement);
        }
    }
}