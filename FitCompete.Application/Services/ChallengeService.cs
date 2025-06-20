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

        // --- Metody READ (bez zmian) ---
        public async Task<IEnumerable<ChallengeDto>> GetAllChallengesAsync()
        {
            var challenges = await _unitOfWork.Repository<Challenge>().GetAllAsync();
            return _mapper.Map<IEnumerable<ChallengeDto>>(challenges);
        }

        public async Task<ChallengeDto?> GetChallengeByIdAsync(int id)
        {
            var challenge = await _unitOfWork.Repository<Challenge>().GetByIdAsync(id);
            if (challenge == null) return null;

            var category = await _unitOfWork.Repository<ChallengeCategory>().GetByIdAsync(challenge.ChallengeCategoryId);
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(challenge.CreatedByUserId);

            var challengeDto = _mapper.Map<ChallengeDto>(challenge);
            challengeDto.CategoryName = category?.Name ?? "N/A";
            challengeDto.CreatedByUserName = user?.UserName ?? "N/A";

            return challengeDto;
        }

        // --- NOWA METODA: CREATE ---
        public async Task<ChallengeDto> CreateChallengeAsync(ChallengeCreateDto challengeDto)
        {
            var challenge = _mapper.Map<Challenge>(challengeDto);

            // Ustawiamy wartości, których nie ma w DTO
            challenge.CreatedDate = DateTime.UtcNow;
            // W prawdziwej aplikacji ID użytkownika wzięlibyśmy z tokena. Na razie hardkodujemy admina (ID=1)
            challenge.CreatedByUserId = 1;

            await _unitOfWork.Repository<Challenge>().AddAsync(challenge);
            await _unitOfWork.CompleteAsync();

            // Zwracamy pełne DTO nowo utworzonego obiektu
            return _mapper.Map<ChallengeDto>(challenge);
        }

        // --- NOWA METODA: UPDATE ---
        public async Task UpdateChallengeAsync(int challengeId, ChallengeUpdateDto challengeDto)
        {
            var challengeToUpdate = await _unitOfWork.Repository<Challenge>().GetByIdAsync(challengeId);
            if (challengeToUpdate == null)
            {
                throw new KeyNotFoundException("Wyzwanie o podanym ID nie istnieje.");
            }

            // AutoMapper zaktualizuje istniejący obiekt challengeToUpdate danymi z challengeDto
            _mapper.Map(challengeDto, challengeToUpdate);

            _unitOfWork.Repository<Challenge>().Update(challengeToUpdate);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<bool> DeleteChallengeAsync(int challengeId)
        {
            var challengeToDelete = await _unitOfWork.Repository<Challenge>().GetByIdAsync(challengeId);
            if (challengeToDelete == null)
            {
                return false; // Nie znaleziono, nie usunięto
            }

            _unitOfWork.Repository<Challenge>().Remove(challengeToDelete);
            await _unitOfWork.CompleteAsync();
            return true; // Pomyślnie usunięto
        }
    }
}