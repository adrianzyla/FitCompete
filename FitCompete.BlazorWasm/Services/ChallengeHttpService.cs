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
    }
}