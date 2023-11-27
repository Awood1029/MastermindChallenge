namespace MastermindChallenge.Blazor.Server.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IClient _httpClient;

        public GameService(IClient httpClient)
        {
            _httpClient = httpClient;
        }
        public string CheckAnswer()
        {
            return "";
        }

        public Task CreateGameAsync(GameCreateDto gameCreateDto)
        {
            var result = _httpClient.CreateGameAsync(gameCreateDto);
            return result;
        }
    }
}
