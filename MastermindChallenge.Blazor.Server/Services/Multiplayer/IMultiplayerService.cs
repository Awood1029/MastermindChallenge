namespace MastermindChallenge.Blazor.Server.Services.Multiplayer
{
    public interface IMultiplayerService 
    {
        Task<Session> CreateSession();
        Task<IEnumerable<Session>> GetSessionsAsync();
    }
}
