namespace MastermindChallenge.Blazor.Server.Services.Multiplayer
{
    public class MultiplayerService : IMultiplayerService
    {
        private readonly IClient _httpClient;
        private readonly ILogger<MultiplayerService> _logger;

        public MultiplayerService(IClient httpClient, ILogger<MultiplayerService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<Session> CreateSession()
        {
            try
            {
                var response = await _httpClient.CreateSessionAsync();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating session.");
            }
            return new Session();
        }

        public async Task<IEnumerable<Session>> GetSessionsAsync()
        {
            try
            {
                var response = await _httpClient.SessionsAsync();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting sessions.");
            }
            return Enumerable.Empty<Session>();
        }
    }
}
