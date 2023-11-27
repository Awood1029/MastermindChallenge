namespace MastermindChallenge.Blazor.Server.Services.Game
{
    public interface IGameService
    {
        Task CreateGameAsync(GameCreateDto gameCreateDto);
        string CheckAnswer();
        int[] GetRandomNumber(int answerLength);
    }
}
