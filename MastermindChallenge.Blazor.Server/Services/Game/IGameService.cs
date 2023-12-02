using MastermindChallenge.Blazor.Server.Models;

namespace MastermindChallenge.Blazor.Server.Services.Game
{
    public interface IGameService
    {
        Task SaveGameAsync(SaveGameDto gameCreateDto);
        Task<int[]> GetRandomNumber(GamePage gamePageModel);
        Task<IEnumerable<Top10ScoresDto>> GetLeaderboard(int difficulty);
        Task HandleGuess(GamePage gamePageModel);
        Task EndGame(bool isWinner, GamePage gamePageModel);
    }
}
