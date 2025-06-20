using AutoMapper;
using FitCompete.SharedKernel.Dtos;
using FitCompete.Domain.Entities;
using FitCompete.Domain.Interfaces;

namespace FitCompete.Application.Services
{
    public class ChallengeService : IChallengeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ChallengeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ChallengeDto>> GetAllChallengesAsync()
        {
            var challenges = await _unitOfWork.Repository<Challenge>().GetAllAsync(
            );
            return _mapper.Map<IEnumerable<ChallengeDto>>(challenges);
        }


        public async Task<ChallengeDto?> GetChallengeByIdAsync(int id)
        {
            var challenges = await _unitOfWork.Repository<Challenge>().FindAsync(c => c.ChallengeId == id);
            var challenge = challenges.FirstOrDefault();

            if (challenge == null) return null;

            if (challenge.ChallengeCategory == null)
                challenge.ChallengeCategory = await _unitOfWork.Repository<ChallengeCategory>().GetByIdAsync(challenge.ChallengeCategoryId);

            if (challenge.CreatedByUser == null)
                challenge.CreatedByUser = await _unitOfWork.Repository<User>().GetByIdAsync(challenge.CreatedByUserId);

            return _mapper.Map<ChallengeDto>(challenge);
        }

        public async Task<ChallengeDto> CreateChallengeAsync(ChallengeCreateDto challengeDto)
        {
            var challenge = _mapper.Map<Challenge>(challengeDto);

            challenge.CreatedDate = DateTime.UtcNow;
            challenge.CreatedByUserId = 1;

            await _unitOfWork.Repository<Challenge>().AddAsync(challenge);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<ChallengeDto>(challenge);
        }

        public async Task UpdateChallengeAsync(int challengeId, ChallengeUpdateDto challengeDto)
        {
            var challengeToUpdate = await _unitOfWork.Repository<Challenge>().GetByIdAsync(challengeId);
            if (challengeToUpdate == null)
            {
                throw new KeyNotFoundException("Wyzwanie o podanym ID nie istnieje.");
            }

            _mapper.Map(challengeDto, challengeToUpdate);

            _unitOfWork.Repository<Challenge>().Update(challengeToUpdate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteChallengeAsync(int challengeId)
        {
            var challengeToDelete = await _unitOfWork.Repository<Challenge>().GetByIdAsync(challengeId);
            if (challengeToDelete == null)
            {
                return false; 
            }

            _unitOfWork.Repository<Challenge>().Remove(challengeToDelete);
            await _unitOfWork.CompleteAsync();
            return true;
        }
        public async Task<IEnumerable<RankingEntryDto>> GetChallengeRankingAsync(int challengeId)
        {
            var challenge = await _unitOfWork.Repository<Challenge>().GetByIdAsync(challengeId);
            if (challenge == null)
            {
                return new List<RankingEntryDto>(); // Zwróć pustą listę, jeśli wyzwanie nie istnieje
            }

            var attempts = await _unitOfWork.Repository<ChallengeAttempt>().FindAsync(a => a.ChallengeId == challengeId);

            var userIds = attempts.Select(a => a.UserId).Distinct();
            var users = (await _unitOfWork.Repository<User>().FindAsync(u => userIds.Contains(u.UserId))).ToDictionary(u => u.UserId, u => u.UserName);

            var sortedAttempts = attempts.OrderBy(a => a.ResultValue);

            var ranking = sortedAttempts.Select(attempt => new RankingEntryDto
            {
                UserName = users.ContainsKey(attempt.UserId) ? users[attempt.UserId] : "Nieznany",
                ResultValue = attempt.ResultValue,
                AttemptDate = attempt.AttemptDate
            }).ToList();

            return ranking;
        }
    }
}