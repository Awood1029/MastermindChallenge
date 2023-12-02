using MastermindChallenge.Blazor.Server.Models;
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

        public async Task SaveGameAsync(SaveGameDto gameDto)
        {

            var result = _httpClient.SaveGameAsync(gameDto);
        }

        public async Task<int[]> GetRandomNumber(GamePage gamePageModel)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetStringAsync("https://www.random.org/integers/?num=4&min=0&max=7&col=1&base=10&format=plain&rnd=new");
            // We get the answer as a single string, so we need to split it into an array of chars to then convert to an array of ints
            var answerStringArr = response.Split("\n");
            int[] answerToGuessArr = new int[gamePageModel.Difficulty];
            for (int i = 0; i < answerStringArr.Length - 1; i++)
            {
                answerToGuessArr[i] = int.Parse(answerStringArr[i]);
            }
            gamePageModel.AnswerToGuess = answerToGuessArr;
            return answerToGuessArr;
        }

        public async Task<IEnumerable<Top10ScoresDto>> GetLeaderboard(int difficulty)
        {
            var response = await _httpClient.LeaderboardAsync(difficulty);

            return response.ToList();
        }

        public async Task HandleGuess(GamePage gamePageModel)
        {
            var numCorrect = 0;
            var positionCorrect = 0;

            if (gamePageModel.PlayerGuessString == null || gamePageModel.PlayerGuessString.Length != gamePageModel.AnswerToGuess.Length)
            {
                gamePageModel.Feedback.Add($"Invalid guess. Please only guess {gamePageModel.AnswerToGuess.Length} digits.");
                return;
            }

            // Takes the string of the player's guess and converts it to an array of ints
            ParsePlayerGuess(gamePageModel);

            // This will be used to improve the efficiency of determining if the player guessed a number correctly
            var answerTracker = CreateAnswerTracker(gamePageModel);

            for (int i = 0; i < gamePageModel.PlayerGuessArr.Length; i++)
            {
                if (answerTracker.ContainsKey(gamePageModel.PlayerGuessArr[i]) && answerTracker[gamePageModel.PlayerGuessArr[i]] > 0)
                {
                    answerTracker[gamePageModel.PlayerGuessArr[i]]--;
                    numCorrect++;
                }
                if (gamePageModel.PlayerGuessArr[i] == gamePageModel.AnswerToGuess[i]) positionCorrect++;
            }

            gamePageModel.GuessCount++;

            if (positionCorrect == gamePageModel.AnswerToGuess.Length)
            {
                gamePageModel.Feedback.Add($"Great job, you got it in {gamePageModel.GuessCount} attempts!");
                gamePageModel.GameWon = true;
                await EndGame(true, gamePageModel);
            }
            else
            {
                gamePageModel.Feedback.Add($"You guessed {gamePageModel.PlayerGuessString} and got {numCorrect} numbers correct and {positionCorrect} positions correct.");
                if (gamePageModel.GuessCount >= 10)
                {
                    gamePageModel.Feedback.Add($"The correct answer was {string.Join("", gamePageModel.AnswerToGuess)} Unfortunately that was your last chance, try playing again");
                    await EndGame(false, gamePageModel);
                }
            }
        }

        private void ParsePlayerGuess(GamePage gamePageModel)
        {
            for (int i = 0; i < gamePageModel.PlayerGuessString.Length; i++)
            {
                if (Char.IsLetter(gamePageModel.PlayerGuessString[i]))
                {
                    gamePageModel.Feedback.Add($"Invalid guess. Please only use numbers.");
                    return;
                }
                gamePageModel.PlayerGuessArr[i] = int.Parse(gamePageModel.PlayerGuessString[i].ToString());
            }
        }

        private Dictionary<int, int> CreateAnswerTracker(GamePage gamePageModel)
        {
            var answerTracker = new Dictionary<int, int>();
            foreach (var num in gamePageModel.AnswerToGuess)
            {
                if (answerTracker.ContainsKey(num))
                {
                    answerTracker[num]++;
                }
                else
                {
                    answerTracker.Add(num, 1);
                }
            }
            return answerTracker;
        }

        public async Task EndGame(bool isWinner, GamePage gamePageModel)
        {
            gamePageModel.GameEnded = true;

            SaveGameDto gameDto = new SaveGameDto();
            // Prepare gameDto for game save
            gameDto.AnswerToGuess = gamePageModel.AnswerToGuess;
            // Hard coding for now until difficulty implemented
            gameDto.Difficulty = gamePageModel.Difficulty;
            gameDto.IsWinner = isWinner;
            gameDto.AttemptsUsed = gamePageModel.GuessCount;

            // Get Current User Id
            var user = (await _authStateProvider.GetAuthenticationStateAsync()).User;
            gameDto.PlayerId = user.FindFirst(u => u.Type.Contains("uid"))?.Value;

            await _httpClient.SaveGameAsync(gameDto);
        }
    }
}
