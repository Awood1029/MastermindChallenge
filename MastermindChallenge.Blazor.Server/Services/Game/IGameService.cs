namespace MastermindChallenge.Blazor.Server.Services.Game
{
    public interface IGameService
    {
        Task SaveGameAsync(SaveGameDto gameCreateDto);
        string CheckAnswer();
        int[] GetRandomNumber(int answerLength);
    }
}
