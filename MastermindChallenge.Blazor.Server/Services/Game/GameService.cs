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

        public Task SaveGameAsync(SaveGameDto gameDto)
        {
            var result = _httpClient.SaveGameAsync(gameDto);
            return result;
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
