using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ChikoRokoBot.Notifier.Clients
{
	public class UserApiClient
	{
        private readonly HttpClient _httpClient;
        private readonly ILogger<UserApiClient> _logger;

        public UserApiClient(HttpClient httpClient, ILogger<UserApiClient> logger)
		{
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task DeleteUser(long userId)
        {
            var result = await _httpClient.DeleteAsync($"api/user/{userId}");
            if (!result.IsSuccessStatusCode)
                _logger.LogError($"Error deleting user: {result.StatusCode} - {result.ReasonPhrase}");
        }
	}
}

