using MastermindChallenge.Blazor.Server.Models;
using MastermindChallenge.Blazor.Server.Providers;

namespace MastermindChallenge.Blazor.Server.Services.Game
{
    public class GameService : IGameService
    {
        private readonly IClient _httpClient;
        private readonly ApiAuthenticationStateProvider _authStateProvider;
        private readonly ILogger<GameService> _logger;

        public GameService(IClient httpClient, ApiAuthenticationStateProvider authStateProvider, ILogger<GameService> logger)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _logger = logger;
        }

        public async Task GetRandomNumber(GamePage gamePageModel)
        {
            try
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
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the random numbers.");
            }
            return;
        }

        public async Task<IEnumerable<Top10ScoresDto>> GetLeaderboard(int difficulty)
        {
            try
            {
                var response = await _httpClient.LeaderboardAsync(difficulty);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the leaderboard.");
                // Handle the error here, such as logging the error or returning a default value
                return Enumerable.Empty<Top10ScoresDto>();
            }
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
                gamePageModel.Feedback.Add($"Great job, the answer was {string.Join("", gamePageModel.AnswerToGuess)} and you got it in {gamePageModel.GuessCount} attempts!");
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

        public async Task EndGame(bool isWinner, GamePage gamePageModel)
        {
            try
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

                _logger.LogInformation("SaveGameAsync operation completed successfully.");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the game.");
                throw;
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

    }
}
