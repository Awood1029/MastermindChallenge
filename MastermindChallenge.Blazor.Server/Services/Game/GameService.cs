using MastermindChallenge.Blazor.Server.Providers;

namespace MastermindChallenge.Blazor.Server.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IClient _httpClient;
        private readonly ApiAuthenticationStateProvider _authStateProvider;

        public GameService(IClient httpClient, ApiAuthenticationStateProvider authStateProvider)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
        }
        public string CheckAnswer()
        {
            return "";
        }

        public async Task SaveGameAsync(SaveGameDto gameDto)
        {
            var user = (await _authStateProvider.GetAuthenticationStateAsync()).User;
            gameDto.PlayerId = user.FindFirst(u => u.Type.Contains("uid"))?.Value;
            var result = _httpClient.SaveGameAsync(gameDto);
        }

        public int[] GetRandomNumber(int answerLength = 4)
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync("https://www.random.org/integers/?num=4&min=0&max=7&col=1&base=10&format=plain&rnd=new").Result.Split("\n");
            int[] answerToGuessArr = new int[answerLength];
            for (int i = 0; i < response.Length - 1; i++)
            {
                answerToGuessArr[i] = int.Parse(response[i]);
            }
            return answerToGuessArr;
        }
    }
}
