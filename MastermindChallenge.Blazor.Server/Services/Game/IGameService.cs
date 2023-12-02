namespace MastermindChallenge.Blazor.Server.Services.Game
{
    public interface IGameService
    {
        Task SaveGameAsync(SaveGameDto gameCreateDto);
        string CheckAnswer();
        Task<int[]> GetRandomNumber(int answerLength);
        Task<IEnumerable<Top10ScoresDto>> GetLeaderboard(int difficulty);
    }
}
