using FitCompete.BlazorWasm.Services;
using FitCompete.SharedKernel.Dtos;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;


namespace FitCompete.BlazorWasm.Services
{
    public class ChallengeHttpService : IChallengeHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ChallengeHttpService> _logger;

        public ChallengeHttpService(HttpClient httpClient, ILogger<ChallengeHttpService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<ChallengeDto>?> GetAllChallengesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<ChallengeDto>>("api/challenges");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all challenges from API.");
                return null;
            }
        }

        public async Task<ChallengeDto?> GetChallengeByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<ChallengeDto>($"api/challenges/{id}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching challenge with ID {ChallengeId} from API.", id);
                return null;
            }
        }

        public async Task<ChallengeAttemptResponseDto?> AddAttemptAsync(ChallengeAttemptRequestDto attempt)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/challengeattempts", attempt);

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("API returned an error: {StatusCode} - {Content}", response.StatusCode, errorContent);
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ChallengeAttemptResponseDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error posting a new attempt to the API.", ex);
                return null;
            }
        }

        public async Task<ChallengeDto?> CreateChallengeAsync(ChallengeCreateDto challenge)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/challenges", challenge);
                if (!response.IsSuccessStatusCode) return null;
                return await response.Content.ReadFromJsonAsync<ChallengeDto>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating a new challenge via API.");
                return null;
            }
        }

        public async Task UpdateChallengeAsync(int challengeId, ChallengeUpdateDto challenge)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"api/challenges/{challengeId}", challenge);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating challenge with ID {ChallengeId} via API.", challengeId);
            }
        }

        public async Task DeleteChallengeAsync(int challengeId)
        {
            try
            {
                await _httpClient.DeleteAsync($"api/challenges/{challengeId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting challenge with ID {ChallengeId} via API.", challengeId);
            }
        }

        public async Task<IEnumerable<ChallengeCategoryDto>?> GetAllCategoriesAsync()
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<ChallengeCategoryDto>>("api/challengecategories");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all categories from API.");
                return null;
            }
        }
        public async Task<IEnumerable<AchievementDto>?> GetAllAchievementsAsync()
        {
            try { return await _httpClient.GetFromJsonAsync<IEnumerable<AchievementDto>>("api/achievements"); }
            catch (Exception ex) { _logger.LogError(ex, "Error getting achievements"); return null; }
        }

        public async Task<AchievementDto?> CreateAchievementAsync(AchievementCreateDto achievementDto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/achievements", achievementDto);
                return await response.Content.ReadFromJsonAsync<AchievementDto>();
            }
            catch (Exception ex) { _logger.LogError(ex, "Error creating achievement"); return null; }
        }
        public async Task<AchievementDto?> GetAchievementByIdAsync(int id)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<AchievementDto>($"api/achievements/{id}");
            }
            catch (Exception ex) { _logger.LogError(ex, "Error fetching achievement with ID {id}.", id); return null; }
        }
        public async Task DeleteAchievementAsync(int id)
        {
            try { await _httpClient.DeleteAsync($"api/achievements/{id}"); }
            catch (Exception ex) { _logger.LogError(ex, "Error deleting achievement"); }
        }

        public async Task UpdateAchievementAsync(int id, AchievementUpdateDto dto)
        {
            try
            {
                await _httpClient.PutAsJsonAsync($"api/achievements/{id}", dto);
            }
            catch (Exception ex) { _logger.LogError(ex, "Error updating achievement {id}.", id); }
        }

        public async Task<RankingEntryDto?> GetAttemptByIdAsync(int attemptId)
        {
            // Ten endpoint nie istnieje jeszcze w API, ale dodamy go w następnym kroku.
            // Na razie zakładamy, że będzie on wyglądał tak:
            try
            {
                // To jest uproszczenie. W realnej aplikacji byłby dedykowany endpoint.
                // Tutaj symulujemy jego działanie.
                // UWAGA: Ten kod nie zadziała, dopóki nie dodamy endpointu w API.
                // Jest to celowo pokazane, by zilustrować potrzebę rozbudowy API.
                // W finalnej wersji ten kod będzie wyglądał inaczej.
                // Na razie zwrócimy null.
                await Task.Delay(10); // Symulacja opóźnienia
                _logger.LogWarning("Endpoint /api/challengeattempts/{id} nie jest zaimplementowany. Zwracam null.", attemptId);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching attempt by id {id}.", attemptId);
                return null;
            }
        }
        public async Task<IEnumerable<RankingEntryDto>?> GetRankingAsync(int challengeId)
        {
            try
            {
                return await _httpClient.GetFromJsonAsync<IEnumerable<RankingEntryDto>>($"api/challenges/{challengeId}/ranking");
            }
            catch (Exception ex)
            {
                // W WASM logowanie trafia do konsoli przeglądarki
                _logger.LogError(ex, "Error fetching ranking for challenge {id}.", challengeId);
                return null;
            }
        }

    }
}